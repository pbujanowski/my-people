using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Infrastructure.Tests.Generators;

public class PostImageDataGenerator : TheoryData<PostImage>
{
    public PostImageDataGenerator()
    {
        Add(new() { Id = Guid.NewGuid() });

        Add(new() { Id = Guid.NewGuid() });
    }
}
