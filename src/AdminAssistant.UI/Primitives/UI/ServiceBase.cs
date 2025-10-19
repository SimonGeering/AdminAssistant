namespace AdminAssistant.Primitives.UI;

internal abstract class ServiceBase
{
    protected IAdminAssistantWebAPIClient AdminAssistantWebAPIClient { get; }
    protected ILoggingProvider Log { get; }

    protected ServiceBase(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, ILoggingProvider log)
    {
        AdminAssistantWebAPIClient = adminAssistantWebAPIClient;
        Log = log;
    }
}
