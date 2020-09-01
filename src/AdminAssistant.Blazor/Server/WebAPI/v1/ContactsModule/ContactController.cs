using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.ContactsModule
{
    [ApiController]
    [Route("api/v1/core/[controller]")]
    [ApiExplorerSettings(GroupName = "Contacts - Contact")]
    public class ContactController : WebAPIControllerBase
    {
        public ContactController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        [HttpGet]
        [SwaggerOperation("Lists all contacts", OperationId = "GetContact")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of ContactResponseDto", type: typeof(IEnumerable<ContactResponseDto>))]
        public async Task<ActionResult<IEnumerable<ContactResponseDto>>> GetContacts()
        {
            Log.Start();

            var result = await Mediator.Send(new ContactQuery()).ConfigureAwait(false);
            var response = Mapper.Map<IEnumerable<ContactResponseDto>>(result.Value);

            return Log.Finish(Ok(response));
        }
    }
}
