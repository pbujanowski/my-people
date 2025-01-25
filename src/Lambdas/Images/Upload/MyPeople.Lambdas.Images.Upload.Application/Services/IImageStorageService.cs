using MyPeople.Common.Models.Dtos;
using MyPeople.Lambdas.Images.Upload.Application.Dtos;

namespace MyPeople.Lambdas.Images.Upload.Application.Services;

public interface IImageStorageService
{
    Task<ImageUploadResponseDto> UploadImageAsync(ImageDto imageDto);
}
