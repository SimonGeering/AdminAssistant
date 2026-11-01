namespace SimonGeering.Framework.Configuration;

public class ConfigurationException : ApplicationBaseException
{
    protected ConfigurationException() : base() {}
    protected ConfigurationException(string? message) : base(message) {}
    protected ConfigurationException(string message, Exception innerException) : base(message, innerException) {}
}
