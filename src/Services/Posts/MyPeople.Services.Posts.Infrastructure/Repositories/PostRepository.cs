using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Domain.Entities;
using MyPeople.Services.Posts.Infrastructure.Data;

namespace MyPeople.Services.Posts.Infrastructure.Repositories;

public class PostRepository(ApplicationDbContext dbContext)
    : RepositoryBase<Post>(dbContext),
        IPostRepository
{
    public IQueryable<Post> FindAllWithPostImages()
    {
        return _dbContext.Set<Post>().Include(e => e.Images).AsNoTracking();
    }
}
