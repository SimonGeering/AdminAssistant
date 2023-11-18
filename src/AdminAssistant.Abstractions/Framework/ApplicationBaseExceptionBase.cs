using System.Runtime.Serialization;

namespace AdminAssistant.Framework;

[Serializable]
public abstract class ApplicationBaseException : ApplicationException
{
    protected ApplicationBaseException(string? message)
        : base(message)
    {
    }

    protected ApplicationBaseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    protected ApplicationBaseException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
