namespace MyPeople.Common.Configuration.Configurations;

public class OidcConfiguration
{
    public string? Issuer { get; set; }

    public string? Audience { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }
}
