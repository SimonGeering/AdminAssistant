using System.Runtime.Serialization;

namespace AdminAssistant.Framework.TypeMapping;

[Serializable]
public class TypeMappingException : ApplicationBaseException
{
    public TypeMappingException(string? message) : base(message)
    {
    }

    protected TypeMappingException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TypeMappingException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
