using Microsoft.Extensions.Logging;
using MyPeople.Common.Models.Dtos;
using MyPeople.Lambdas.Common.Dtos;
using MyPeople.Lambdas.Common.Services;
using MyPeople.Lambdas.Images.Upload.Application.Dtos;
using MyPeople.Lambdas.Images.Upload.Application.Services;
using MyPeople.Lambdas.Images.Upload.Infrastructure.Configurations;

namespace MyPeople.Lambdas.Images.Upload.Infrastructure.Services;

public class ImageStorageService(
    ILogger<ImageStorageService> logger,
    IStorageService storageService,
    ImagesAwsConfiguration imagesAwsConfiguration
) : IImageStorageService
{
    public async Task<ImageUploadResponseDto> UploadImageAsync(ImageDto imageDto)
    {
        var response = new ImageUploadResponseDto();

        try
        {
            logger.LogInformation("Image uploading started.");

            using var imageStream = new MemoryStream(
                Convert.FromBase64String(
                    imageDto.Content
                        ?? throw new InvalidOperationException("Image content is null.")
                )
            );

            var s3Object = new S3ObjectDto
            {
                Name = imageDto?.Name ?? throw new InvalidOperationException("Image name is null."),
                BucketName =
                    imagesAwsConfiguration?.ImageStorage?.BucketName
                    ?? throw new InvalidOperationException("Bucket name is null."),
                InputStream = imageStream,
            };

            var s3Response = await storageService.UploadFileAsync(s3Object);

            logger.LogInformation("Image uploading response: {@Response}.", s3Response);

            response.StatusCode = s3Response.StatusCode;
            response.Message = s3Response.Message;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Image uploading failed.");
            response.StatusCode = 500;
            response.Message = $"Image uploading failed. Message: {exception.Message}";
        }

        return response;
    }
}
