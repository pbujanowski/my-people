using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Application.Wrappers;
using MyPeople.Services.Posts.Infrastructure.Data;

namespace MyPeople.Services.Posts.Infrastructure.Wrappers;

public class RepositoryWrapper(ApplicationDbContext dbContext, IPostRepository posts)
    : IRepositoryWrapper
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public IPostRepository Posts { get; } = posts;

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
