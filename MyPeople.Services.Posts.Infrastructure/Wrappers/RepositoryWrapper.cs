using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Application.Wrappers;
using MyPeople.Services.Posts.Infrastructure.Data;

namespace MyPeople.Services.Posts.Infrastructure.Wrappers;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly ApplicationDbContext _dbContext;

    public IPostRepository Posts { get; }

    public RepositoryWrapper(ApplicationDbContext dbContext, IPostRepository posts)
    {
        _dbContext = dbContext;
        Posts = posts;
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}