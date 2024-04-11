namespace MyPeople.Common.Configuration.Exceptions;

public class ConfigurationException : Exception
{
    public ConfigurationException() { }

    public ConfigurationException(string? configurationKey)
        : base($"Configuration '{configurationKey}' not found.") { }

    public ConfigurationException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
