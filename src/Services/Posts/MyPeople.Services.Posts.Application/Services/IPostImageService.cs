using MyPeople.Services.Posts.Application.Dtos;

namespace MyPeople.Services.Posts.Application.Services;

public interface IPostImageService
{
    Task<IEnumerable<PostImageDto>> CreatePostImages(
        IEnumerable<CreatePostImageDto> createPostImageDtos
    );
}
