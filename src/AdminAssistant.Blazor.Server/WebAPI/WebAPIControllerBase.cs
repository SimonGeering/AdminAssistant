using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.WebAPI
{
    // TODO: may not need this due to code in startup.
    //[Authorize]
    public abstract class WebAPIControllerBase : ControllerBase
    {
        // TODO: expand basic Authorize with policy to differentiate a user from an admin. 
        public WebAPIControllerBase(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
        {
            Mapper = mapper;
            Mediator = mediator;
            Log = loggingProvider;
        }

        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }
        protected ILoggingProvider Log { get; }
    }
}
