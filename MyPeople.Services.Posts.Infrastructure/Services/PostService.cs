using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Posts.Application.Dtos;
using MyPeople.Services.Posts.Application.Services;
using MyPeople.Services.Posts.Application.Wrappers;

namespace MyPeople.Services.Posts.Infrastructure.Services;

public class PostService : IPostService
{
    private readonly IRepositoryWrapper _repositories;
    private readonly IMapper _mapper;

    public PostService(IRepositoryWrapper repositories, IMapper mapper)
    {
        _repositories = repositories;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
    {
        var entities = await _repositories.Posts.FindAll().ToListAsync();
        return _mapper.Map<IEnumerable<PostDto>>(entities);
    }
}
