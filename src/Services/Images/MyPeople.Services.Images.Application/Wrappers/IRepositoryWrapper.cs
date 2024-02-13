using MyPeople.Services.Images.Application.Repositories;

namespace MyPeople.Services.Images.Application.Wrappers;

public interface IRepositoryWrapper
{
    IImageRepository Images { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}