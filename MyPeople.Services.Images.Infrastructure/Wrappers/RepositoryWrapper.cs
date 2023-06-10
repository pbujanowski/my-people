using MyPeople.Services.Images.Application.Repositories;
using MyPeople.Services.Images.Application.Wrappers;
using MyPeople.Services.Images.Infrastructure.Data;

namespace MyPeople.Services.Images.Infrastructure.Wrappers
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _dbContext;

        public IImageRepository Images { get; }

        public RepositoryWrapper(ApplicationDbContext dbContext, IImageRepository images)
        {
            _dbContext = dbContext;
            Images = images;
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
}