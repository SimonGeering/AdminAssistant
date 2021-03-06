using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.WebAPI
{
    public abstract class WebAPIControllerBase : ControllerBase
    {
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
