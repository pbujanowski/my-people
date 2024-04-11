using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Posts.Application.Wrappers;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Infrastructure.Services;

public class PostService(
    IRepositoryWrapper repositories,
    IImageService imageService,
    IMapper mapper,
    IConfiguration configuration
) : IPostService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IImageService _imageService = imageService;
    private readonly IMapper _mapper = mapper;
    private readonly IRepositoryWrapper _repositories = repositories;

    public async Task<PostDto?> CreatePostAsync(CreatePostDto postDto)
    {
        var entity = _mapper.Map<Post>(postDto);
        var createdEntity = _repositories.Posts.Create(entity);
        if (postDto.Images?.Count > 0)
        {
            var images = await _imageService.CreateImagesAsync(postDto.Images);
            if (images is not null)
                foreach (var image in images)
                    _repositories.PostImages.Create(
                        new PostImage { PostId = createdEntity.Id, ImageId = image.Id }
                    );
        }

        await _repositories.SaveChangesAsync();
        return _mapper.Map<PostDto>(createdEntity);
    }

    public async Task<PostDto?> DeletePostAsync(DeletePostDto postDto)
    {
        var entity = _mapper.Map<Post>(postDto);

        var postImagesToDelete = _repositories.PostImages.FindByPostId(entity.Id);
        var anyPostImages = await postImagesToDelete.AnyAsync();
        if (anyPostImages)
        {
            _repositories.PostImages.Delete(postImagesToDelete);
            var deletedImages =
                await _imageService.DeleteImagesAsync(
                    postImagesToDelete.Select(x => new DeleteImageDto { Id = x.ImageId })
                )
                ?? throw new Exception(
                    $"Images to delete not found. Images IDs: {postImagesToDelete.Select(x => x.ImageId)}."
                );
        }

        var deletedEntity = _repositories.Posts.Delete(entity);
        await _repositories.SaveChangesAsync();
        return _mapper.Map<PostDto>(deletedEntity);
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        var entities = await _repositories.Posts.FindAllWithPostImages().ToListAsync();
        var dtos = _mapper.Map<ICollection<PostDto>>(entities);
        foreach (var dto in dtos)
        {
            var entity = entities.FirstOrDefault(x => x.Id == dto.Id)!;
            var anyImages = entity.Images?.Count > 0;
            if (anyImages)
            {
                var gatewayWebUrl = _configuration.GetSection("Gateways:Web:Url").Get<string>();
                dto.Images = [];
                foreach (var image in entity.Images!)
                    dto.Images.Add(
                        new PostImageDto
                        {
                            ImageId = image.ImageId,
                            Url = $"{gatewayWebUrl}/images/browse/{image.ImageId}"
                        }
                    );
            }
        }

        return dtos;
    }

    public async Task<PostDto?> GetPostByIdAsync(Guid id)
    {
        var entity = await _repositories
            .Posts.FindByCondition(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return entity is null ? null : _mapper.Map<PostDto>(entity);
    }

    public async Task<PostDto?> UpdatePostAsync(UpdatePostDto postDto)
    {
        var entity = _mapper.Map<Post>(postDto);
        var updatedEntity = _repositories.Posts.Update(entity);
        await _repositories.SaveChangesAsync();
        return _mapper.Map<PostDto>(updatedEntity);
    }
}
