namespace AdminAssistant.Primitives.UI;

internal abstract class ServiceBase
{
    protected IAdminAssistantWebAPIClient AdminAssistantWebAPIClient { get; }
    protected IMapper Mapper { get; }
    protected ILoggingProvider Log { get; }

    protected ServiceBase(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, IMapper mapper, ILoggingProvider log)
    {
        AdminAssistantWebAPIClient = adminAssistantWebAPIClient;
        Log = log;
        Mapper = mapper;
    }
}
