using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Domain.Entities;
using MyPeople.Services.Posts.Infrastructure.Data;

namespace MyPeople.Services.Posts.Infrastructure.Repositories;

public class PostImageRepository(ApplicationDbContext dbContext)
    : RepositoryBase<PostImage>(dbContext),
        IPostImageRepository
{
    public IQueryable<PostImage> FindByPostId(Guid? postId)
    {
        return FindByCondition(e => e.PostId == postId);
    }
}