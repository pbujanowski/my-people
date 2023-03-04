using OpenIddict.EntityFrameworkCore.Models;

namespace MyPeople.Identity.Infrastructure.Data;

public class ApplicationAuthorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, ApplicationClient, ApplicationToken>
{
}