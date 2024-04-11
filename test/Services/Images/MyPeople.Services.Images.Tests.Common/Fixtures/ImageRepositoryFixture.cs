using MyPeople.Services.Images.Application.Repositories;
using MyPeople.Services.Images.Infrastructure.Repositories;

namespace MyPeople.Services.Images.Tests.Common.Fixtures;

public class ImageRepositoryFixture : ApplicationDbContextFixture
{
    public IImageRepository ImageRepository { get; }

    public ImageRepositoryFixture()
    {
        ImageRepository = new ImageRepository(DbContext);
    }
}
