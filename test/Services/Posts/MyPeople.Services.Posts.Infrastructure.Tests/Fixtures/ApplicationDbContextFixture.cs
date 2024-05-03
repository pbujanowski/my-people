using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Posts.Domain.Entities;
using MyPeople.Services.Posts.Infrastructure.Data;

namespace MyPeople.Services.Posts.Infrastructure.Tests.Fixtures;

public class ApplicationDbContextFixture : IAsyncDisposable
{
    public ApplicationDbContext DbContext { get; }

    public ObjectsComparer.Comparer<Post> PostComparer { get; }

    public ObjectsComparer.Comparer<PostImage> PostImageComparer { get; }

    public ApplicationDbContextFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("MyPeople.Services.Posts.Infrastructure.Tests")
            .Options;

        DbContext = new ApplicationDbContext(options);

        DbContext.Database.EnsureCreated();

        PostComparer = new ObjectsComparer.Comparer<Post>();
        PostImageComparer = new ObjectsComparer.Comparer<PostImage>();
    }

    public async ValueTask DisposeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}
