namespace AdminAssistant.Primitives.UI;

internal abstract class ServiceBase
{
    protected ILoggingProvider Log { get; }

    protected ServiceBase(ILoggingProvider log) => Log = log;
}
