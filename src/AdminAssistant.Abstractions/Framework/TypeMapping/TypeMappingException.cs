using System.Runtime.Serialization;

namespace AdminAssistant.Framework.TypeMapping;

public class TypeMappingException(string? message)
    : ApplicationBaseException(message) { }
