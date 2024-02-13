using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyPeople.Common.Configuration.Configurations;
using MyPeople.Common.Configuration.Exceptions;

namespace MyPeople.Services.Common.Services;

public class ServiceDiscoveryHostedService(IConsulClient consulClient, IConfiguration configuration)
    : IHostedService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IConsulClient _consulClient = consulClient;
    private AgentServiceRegistration? _registration;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var serviceDiscoveryConfiguration =
            _configuration.GetSection("ServiceDiscovery").Get<ServiceDiscoveryConfiguration>()
            ?? throw new ConfigurationException("ServiceDiscovery");

        _registration = new AgentServiceRegistration
        {
            ID = serviceDiscoveryConfiguration.Id,
            Name = serviceDiscoveryConfiguration.Name,
            Address = serviceDiscoveryConfiguration.Address,
            Port = serviceDiscoveryConfiguration.Port ?? 0,
            Check = new AgentServiceCheck
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                Interval = TimeSpan.FromSeconds(15),
                HTTP =
                    $"http://{serviceDiscoveryConfiguration.Address}:{serviceDiscoveryConfiguration.Port}/{serviceDiscoveryConfiguration.HealthCheckEndpoint}",
                Timeout = TimeSpan.FromSeconds(5)
            }
        };

        await _consulClient
            .Agent.ServiceDeregister(_registration.ID, cancellationToken)
            .ConfigureAwait(false);

        await _consulClient
            .Agent.ServiceRegister(_registration, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _consulClient
            .Agent.ServiceDeregister(_registration?.ID, cancellationToken)
            .ConfigureAwait(false);
    }
}