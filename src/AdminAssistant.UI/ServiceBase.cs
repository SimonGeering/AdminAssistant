using AutoMapper;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.UI
{
    public abstract class ServiceBase
    {
        protected IAdminAssistantWebAPIClient AdminAssistantWebAPIClient { get; }
        protected IMapper Mapper { get; }
        protected ILoggingProvider Log { get; }

        public ServiceBase(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, IMapper mapper, ILoggingProvider log)
        {
            AdminAssistantWebAPIClient = adminAssistantWebAPIClient;
            Log = log;
            Mapper = mapper;
        }
    }
}
