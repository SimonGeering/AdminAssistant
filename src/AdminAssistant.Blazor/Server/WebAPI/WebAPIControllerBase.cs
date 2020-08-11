using AdminAssistant.Framework.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.WebAPI.v1
{
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
