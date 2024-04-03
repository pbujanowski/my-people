using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Infrastructure.Data;

namespace MyPeople.Services.Images.Infrastructure.Tests.Fixtures;

public class ApplicationDbContextFixture : IAsyncDisposable
{
    public ApplicationDbContext DbContext { get; }
    
    public ObjectsComparer.Comparer<Image> ImageComparer { get; }

    public ApplicationDbContextFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("MyPeople.Services.Images.Infrastructure.Tests")
            .Options;

        DbContext = new ApplicationDbContext(options);
        
        DbContext.Database.EnsureCreated();

        ImageComparer = new ObjectsComparer.Comparer<Image>();
    }

    public async ValueTask DisposeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.DisposeAsync();
        
        GC.SuppressFinalize(this);
    }
}