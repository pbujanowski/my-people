using MyPeople.Services.Images.Application.Dtos;

namespace MyPeople.Services.Images.Application.Services
{
    public interface IImageService
    {
        Task<ImageDto?> CreateImageAsync(ImageDto imageDto);

        Task<ImageDto?> GetImageByIdAsync(Guid id);
    }
}
