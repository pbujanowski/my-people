using MyPeople.Common.Models.Dtos;

namespace MyPeople.Common.Abstractions.Services;

public interface IImageService
{
    Task<ImageDto?> CreateImageAsync(CreateImageDto imageDto);

    Task<IEnumerable<ImageDto>?> CreateImagesAsync(IEnumerable<CreateImageDto> imagesDtos);

    Task<IEnumerable<ImageDto>?> DeleteImagesAsync(IEnumerable<DeleteImageDto> imagesDtos);

    Task<ImageDto?> GetImageByIdAsync(Guid id);

    Task<IEnumerable<ImageDto>?> GetImagesByIdsAsync(IEnumerable<Guid> ids);
}