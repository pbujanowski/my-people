using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Domain.Entities;
using MyPeople.Services.Posts.Infrastructure.Data;

namespace MyPeople.Services.Posts.Infrastructure.Repositories;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}