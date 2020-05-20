using AdminAssistant.Framework.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.WebAPI.v1
{
    // TODO: replace this manual method with Microsoft.AspNetCore.Mvc.Versioning - See: https://stackoverflow.com/questions/51710388/nswag-net-core-api-versioning-configuration
    [Route("api/v1/[controller]")]
    public abstract class WebAPIControllerBase : ControllerBase
    {
        public WebAPIControllerBase(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
            this.Log = loggingProvider;
        }

        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }
        protected ILoggingProvider Log { get; }
    }
}
