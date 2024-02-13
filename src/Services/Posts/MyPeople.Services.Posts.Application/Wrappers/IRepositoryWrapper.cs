using MyPeople.Services.Posts.Application.Repositories;

namespace MyPeople.Services.Posts.Application.Wrappers;

public interface IRepositoryWrapper
{
    IPostRepository Posts { get; }

    IPostImageRepository PostImages { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}