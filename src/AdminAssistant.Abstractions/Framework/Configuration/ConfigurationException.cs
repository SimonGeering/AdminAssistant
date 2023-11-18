using System.Runtime.Serialization;

namespace AdminAssistant.Framework.Configuration;

[Serializable]
public class ConfigurationException : ApplicationBaseException
{
    public ConfigurationException(string? message) : base(message)
    {
    }

    protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ConfigurationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
