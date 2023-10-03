namespace MyPeople.Common.Configuration.Exceptions;

public class ConfigurationException : Exception
{
    public ConfigurationException()
        : base() { }

    public ConfigurationException(string? configurationKey)
        : base($"Configuration '{configurationKey}' not found.") { }

    public ConfigurationException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
