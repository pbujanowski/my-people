using System.Text;
using System.Text.Json;
using MyPeople.Common.Models.Dtos;
using MyPeople.Lambdas.Common.Dtos;
using MyPeople.Lambdas.Common.Services;
using MyPeople.Lambdas.Images.Application.Dtos;
using MyPeople.Lambdas.Images.Application.Services;

namespace MyPeople.Lambdas.Images.Infrastructure.Services;

public class ImageStorageService(IStorageService storageService) : IImageStorageService
{
    public async Task<ImageUploadResponseDto> UploadImageAsync(string imageJson)
    {
        var response = new ImageUploadResponseDto();
        ImageDto? imageDto = null;

        try
        {
            imageDto = JsonSerializer.Deserialize<ImageDto>(imageJson);
            if (imageDto is null)
            {
                throw new InvalidOperationException("Deserialized image is null.");
            }
        }
        catch (Exception exception)
        {
            response.StatusCode = 500;
            response.Message = $"Image deserialization failed. Message: {exception.Message}";
        }

        using var imageStream = new MemoryStream(
            Encoding.UTF8.GetBytes(
                imageDto!.Content ?? throw new InvalidOperationException("Image content is null.")
            )
        );

        var s3Object = new S3ObjectDto
        {
            Name = imageDto!.Name ?? throw new InvalidOperationException("Image name is null."),
            BucketName = "",
            InputStream = imageStream,
        };

        var s3Response = await storageService.UploadFileAsync(s3Object);

        response.StatusCode = s3Response.StatusCode;
        response.Message = s3Response.Message;

        return response;
    }
}
