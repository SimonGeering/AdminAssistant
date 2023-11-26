using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

[ApiController]
[Route("api/v1/contacts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Contacts Module")]
public sealed class ContactController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpPut]
    [SwaggerOperation("Update an existing Contact.", OperationId = "PutContact")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated ContactResponseDto", type: typeof(ContactResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the ContactID of the given ContactUpdateRequest does not exist.")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given ContactUpdateRequest is invalid.")]
    public async Task<ActionResult<ContactResponseDto>> ContactPut([FromBody, SwaggerParameter("The Contact for which updates are to be persisted.", Required = true)] ContactUpdateRequestDto ContactUpdateRequest, CancellationToken cancellationToken)
    {
        Log.Start();

        var contact = Mapper.Map<Contact>(ContactUpdateRequest);
        var result = await Mediator.Send(new ContactUpdateCommand(contact), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return Log.Finish(NotFound(ModelState));
        }

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return Log.Finish(UnprocessableEntity(ModelState));
        }

        var response = Mapper.Map<ContactResponseDto>(result.Value);
        return Log.Finish(Ok(response));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new Contact.", OperationId = "PostContact")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created Contact with its assigned newly ID.", type: typeof(ContactResponseDto))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given ContactCreateRequest is invalid.")]
    public async Task<ActionResult<ContactResponseDto>> ContactPost([FromBody, SwaggerParameter("The details of the Contact to be created.", Required = true)] ContactCreateRequestDto ContactCreateRequest, CancellationToken cancellationToken)
    {
        Log.Start();

        var contact = Mapper.Map<Contact>(ContactCreateRequest);
        var result = await Mediator.Send(new ContactCreateCommand(contact), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return Log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
        }

        var response = Mapper.Map<ContactResponseDto>(result.Value);
        return Log.Finish(CreatedAtRoute(nameof(ContactGetById), new { response.ContactID }, response));
    }

    [HttpGet("{contactID}", Name = nameof(ContactGetById))]
    [SwaggerOperation("Gets the Contact with the given ID.", OperationId = "GetContactById")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK - returns the Contact requested.", type: typeof(ContactResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given ContactID does not exist.")]
    public async Task<ActionResult<ContactResponseDto>> ContactGetById([SwaggerParameter("The ID of the Contact to be returned.", Required = true)] int contactId, CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new ContactByIDQuery(contactId), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return Log.Finish(NotFound());

        var response = Mapper.Map<ContactResponseDto>(result.Value);
        return Log.Finish(Ok(response));
    }

    [HttpGet]
    [SwaggerOperation("Lists all contacts", OperationId = "GetContact")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of ContactResponseDto", type: typeof(IEnumerable<ContactResponseDto>))]
    public async Task<ActionResult<IEnumerable<ContactResponseDto>>> GetContact(CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new ContactQuery(), cancellationToken).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<ContactResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
