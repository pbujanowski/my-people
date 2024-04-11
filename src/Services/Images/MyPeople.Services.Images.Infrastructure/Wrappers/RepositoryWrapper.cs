using MyPeople.Services.Images.Application.Repositories;
using MyPeople.Services.Images.Application.Wrappers;
using MyPeople.Services.Images.Infrastructure.Data;

namespace MyPeople.Services.Images.Infrastructure.Wrappers;

public class RepositoryWrapper(ApplicationDbContext dbContext, IImageRepository images)
    : IRepositoryWrapper
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public IImageRepository Images { get; } = images;

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
