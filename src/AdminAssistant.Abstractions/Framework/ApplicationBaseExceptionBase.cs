namespace AdminAssistant.Framework;

public abstract class ApplicationBaseException : ApplicationException
{
    protected ApplicationBaseException(string? message)
        : base(message)
    {
    }
}
