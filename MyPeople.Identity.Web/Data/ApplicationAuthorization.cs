using OpenIddict.EntityFrameworkCore.Models;

namespace MyPeople.Identity.Web.Data;

public class ApplicationAuthorization : OpenIddictEntityFrameworkCoreAuthorization<string, ApplicationClient, ApplicationToken>
{
}
