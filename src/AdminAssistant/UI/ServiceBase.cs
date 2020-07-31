using AutoMapper;
using AdminAssistant.Framework.Providers;
using System.Net.Http;

namespace AdminAssistant.UI
{
    public abstract class ServiceBase
    {
        protected IHttpClientProvider HttpClient { get; }
        protected IMapper Mapper { get; }
        protected ILoggingProvider Log { get; }

        public ServiceBase(IHttpClientProvider httpClient, IMapper mapper, ILoggingProvider log)
        {
            this.HttpClient = httpClient;
            this.Log = log;
            this.Mapper = mapper;
        }
    }
}
