using OpenIddict.EntityFrameworkCore.Models;

namespace MyPeople.Identity.Infrastructure.Data;

public class ApplicationToken
    : OpenIddictEntityFrameworkCoreToken<Guid, ApplicationClient, ApplicationAuthorization>
{
}