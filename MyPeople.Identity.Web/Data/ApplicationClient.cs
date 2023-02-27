using OpenIddict.EntityFrameworkCore.Models;

namespace MyPeople.Identity.Web.Data;

public class ApplicationClient : OpenIddictEntityFrameworkCoreApplication<Guid, ApplicationAuthorization, ApplicationToken>
{
}