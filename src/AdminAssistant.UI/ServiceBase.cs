using AutoMapper;
using AdminAssistant.Framework.Providers;
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
            this.AdminAssistantWebAPIClient = adminAssistantWebAPIClient;
            this.Log = log;
            this.Mapper = mapper;
        }
    }
}
