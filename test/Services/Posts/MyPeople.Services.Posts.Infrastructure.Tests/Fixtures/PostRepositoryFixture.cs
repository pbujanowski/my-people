using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Infrastructure.Repositories;

namespace MyPeople.Services.Posts.Infrastructure.Tests.Fixtures;

public class PostRepositoryFixture : ApplicationDbContextFixture
{
    public IPostRepository PostRepository { get; }

    public PostRepositoryFixture()
    {
        PostRepository = new PostRepository(DbContext);
    }
}
