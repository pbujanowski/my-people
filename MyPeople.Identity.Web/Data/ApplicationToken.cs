using OpenIddict.EntityFrameworkCore.Models;

namespace MyPeople.Identity.Web.Data;

public class ApplicationToken : OpenIddictEntityFrameworkCoreToken<string, ApplicationClient, ApplicationAuthorization>
{
}
