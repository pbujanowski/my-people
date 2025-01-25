using Microsoft.Extensions.Configuration;

namespace MyPeople.Common.Configuration.Extensions;

public static class ConfigurationExtensions
{
    public static ConfigurationManager ConfigureConfigurationProviders(
        this ConfigurationManager configuration
    )
    {
        configuration.AddEnvironmentVariables();

        return configuration;
    }
}
