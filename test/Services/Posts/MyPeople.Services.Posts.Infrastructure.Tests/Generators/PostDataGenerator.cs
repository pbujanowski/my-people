using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Infrastructure.Tests.Generators;

public class PostDataGenerator : TheoryData<Post>
{
    public PostDataGenerator()
    {
        Add(
            new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Content = "content1",
                Images = null
            }
        );

        Add(
            new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Content = "content2",
                Images = null
            }
        );
    }
}
