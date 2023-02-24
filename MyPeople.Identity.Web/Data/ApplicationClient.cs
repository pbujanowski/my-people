using System.Net.Mime;
using OpenIddict.EntityFrameworkCore.Models;

namespace MyPeople.Identity.Web.Data;

public class ApplicationClient : OpenIddictEntityFrameworkCoreApplication<string, ApplicationAuthorization, ApplicationToken>
{
}
