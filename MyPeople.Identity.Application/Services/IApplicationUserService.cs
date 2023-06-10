using MyPeople.Identity.Application.Dtos;

namespace MyPeople.Identity.Application.Services;

public interface IApplicationUserService
{
    Task<ApplicationUserDto?> GetUserByIdAsync(Guid id);
}