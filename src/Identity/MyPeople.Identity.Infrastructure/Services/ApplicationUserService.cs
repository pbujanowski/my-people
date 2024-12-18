using MyPeople.Identity.Application.Dtos;
using MyPeople.Identity.Application.Services;
using MyPeople.Identity.Infrastructure.Data;

namespace MyPeople.Identity.Infrastructure.Services;

public class ApplicationUserService(ApplicationDbContext dbContext) : IApplicationUserService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ApplicationUserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user is null)
            return null;

        return new ApplicationUserDto
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
        };
    }
}
