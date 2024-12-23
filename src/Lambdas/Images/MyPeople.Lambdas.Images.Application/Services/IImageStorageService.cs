using MyPeople.Lambdas.Images.Application.Dtos;

namespace MyPeople.Lambdas.Images.Application.Services;

public interface IImageStorageService
{
    Task<ImageUploadResponseDto> UploadImageAsync(string imageJson);
}
