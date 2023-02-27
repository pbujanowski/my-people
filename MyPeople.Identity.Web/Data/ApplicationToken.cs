using OpenIddict.EntityFrameworkCore.Models;

namespace MyPeople.Identity.Web.Data;

public class ApplicationToken : OpenIddictEntityFrameworkCoreToken<Guid, ApplicationClient, ApplicationAuthorization>
{
}