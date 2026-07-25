namespace SimonGeering.Framework;

public abstract class ApplicationBaseException : Exception
{
    protected ApplicationBaseException() : base() {}
    protected ApplicationBaseException(string? message) : base(message) {}
    protected ApplicationBaseException(string message, Exception innerException) : base(message, innerException) {}
}
