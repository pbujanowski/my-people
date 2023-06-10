namespace MyPeople.Services.Posts.Application.Services;

public interface IUserService
{
    Task<string?> GetUserDisplayNameById(Guid? userId);
}