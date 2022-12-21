using System;
using AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

[ApiController]
[Route("api/v1/contacts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Contacts Module")]
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

    [HttpGet("{contactID}", Name = nameof(ContactGetById))]
    [SwaggerOperation("Gets the Contact with the given ID.", OperationId = "GetContactById")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK - returns the Contact requested.", type: typeof(ContactResponseDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "BadRequest - When the given ContactID is invalid.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given ContactID does not exist.")]
    public async Task<ActionResult<ContactResponseDto>> ContactGetById([SwaggerParameter("The ID of the Contact to be returned.", Required = true)] int contactId)
    {
        // WIP
        //return BadRequest(new ContactResponseDto());
        return Ok(new ContactResponseDto());
    }
}
