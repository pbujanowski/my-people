using OpenIddict.EntityFrameworkCore.Models;

namespace MyPeople.Identity.Infrastructure.Data;

public class ApplicationClient
    : OpenIddictEntityFrameworkCoreApplication<Guid, ApplicationAuthorization, ApplicationToken> { }
