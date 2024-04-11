using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPeople.Identity.Domain.Entities;

namespace MyPeople.Identity.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseOpenIddict<
            ApplicationClient,
            ApplicationAuthorization,
            ApplicationScope,
            ApplicationToken,
            Guid
        >();
    }
}
