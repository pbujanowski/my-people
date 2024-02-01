namespace MyPeople.Common.Configuration.Configurations;

public class ServiceDiscoveryConfiguration
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? Port { get; set; }

    public string? DiscoveryAddress { get; set; }

    public string? HealthCheckEndpoint { get; set; }
}
