using MyPeople.Common.Models.Dtos;

namespace MyPeople.Lambdas.Images.Upload.Application.Services;

public interface IImageFetchService
{
    Task<IEnumerable<ImageDto>?> FetchImagesByIds(IEnumerable<Guid> imagesIds);
}
