using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Application.Repositories;

public interface IPostImageRepository : IRepository<PostImage>
{
    IQueryable<PostImage> FindByPostId(Guid? postId);
}
