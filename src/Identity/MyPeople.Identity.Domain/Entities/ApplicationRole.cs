using Microsoft.AspNetCore.Identity;

namespace MyPeople.Identity.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() { }

    public ApplicationRole(string name)
    {
        Name = name;
    }
}
