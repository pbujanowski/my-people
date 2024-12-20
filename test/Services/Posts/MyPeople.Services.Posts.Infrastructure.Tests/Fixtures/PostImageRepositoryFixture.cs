using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Infrastructure.Repositories;

namespace MyPeople.Services.Posts.Infrastructure.Tests.Fixtures;

public class PostImageRepositoryFixture : ApplicationDbContextFixture
{
    public IPostImageRepository PostImageRepository { get; }

    public PostImageRepositoryFixture()
    {
        PostImageRepository = new PostImageRepository(DbContext);
    }
}
