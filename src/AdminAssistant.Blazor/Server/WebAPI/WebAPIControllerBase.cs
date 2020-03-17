using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Blazor.Server.WebAPI.v1
{
    // TODO: replace this manual method with Microsoft.AspNetCore.Mvc.Versioning - See: https://stackoverflow.com/questions/51710388/nswag-net-core-api-versioning-configuration
    [Route("api/v1/[controller]")]
    public abstract class WebAPIControllerBase : ControllerBase
    {
        public WebAPIControllerBase(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        protected IMapper Mapper { get; private set; }
        protected IMediator Mediator { get; private set; }
    }
}
