using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Posts.Application.Wrappers;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Infrastructure.Services;

public class PostService(IRepositoryWrapper repositories, IMapper mapper) : IPostService
{
    private readonly IRepositoryWrapper _repositories = repositories;
    private readonly IMapper _mapper = mapper;

    public async Task<PostDto?> CreatePostAsync(CreatePostDto postDto)
    {
        var entity = _mapper.Map<Post>(postDto);
        var createdEntity = _repositories.Posts.Create(entity);
        await _repositories.SaveChangesAsync();
        return _mapper.Map<PostDto>(createdEntity);
    }

    public async Task<PostDto?> DeletePostAsync(DeletePostDto postDto)
    {
        var entity = _mapper.Map<Post>(postDto);
        var deletedEntity = _repositories.Posts.Delete(entity);
        await _repositories.SaveChangesAsync();
        return _mapper.Map<PostDto>(deletedEntity);
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        var entities = await _repositories.Posts.FindAll().ToListAsync();
        return _mapper.Map<IEnumerable<PostDto>>(entities);
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
