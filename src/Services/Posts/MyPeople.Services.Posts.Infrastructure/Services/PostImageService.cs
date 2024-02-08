using AutoMapper;
using MyPeople.Services.Posts.Application;
using MyPeople.Services.Posts.Application.Dtos;
using MyPeople.Services.Posts.Application.Services;
using MyPeople.Services.Posts.Application.Wrappers;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Infrastructure.Services;

public class PostImageService(IRepositoryWrapper repositories, IMapper mapper) : IPostImageService
{
    private readonly IRepositoryWrapper _repositories = repositories;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PostImageDto>> CreatePostImages(
        IEnumerable<CreatePostImageDto> createPostImageDtos
    )
    {
        var postImageDtos = new List<PostImageDto>();
        foreach (var createPostImageDto in createPostImageDtos)
        {
            var entity = _mapper.Map<PostImage>(createPostImageDto);
            var createdEntity = _repositories.PostImages.Create(entity);
            postImageDtos.Add(_mapper.Map<PostImageDto>(createdEntity));
        }
        await _repositories.SaveChangesAsync();
        return postImageDtos;
    }
}
