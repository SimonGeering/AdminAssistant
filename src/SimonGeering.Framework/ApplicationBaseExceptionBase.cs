namespace SimonGeering.Framework;

public abstract class ApplicationBaseException : Exception
{
    protected ApplicationBaseException(string? message)
        : base(message)
    {
    }
}
