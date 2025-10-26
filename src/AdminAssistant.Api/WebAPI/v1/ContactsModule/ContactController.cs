using AdminAssistant.Modules.ContactsModule.Commands;
using AdminAssistant.Modules.ContactsModule.Queries;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

[ApiController]
[Route("api/v1/contacts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Contacts Module")]
public sealed class ContactController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpPut]
    [SwaggerOperation("Update an existing Contact.", OperationId = "PutContact")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated ContactResponseDto", type: typeof(ContactResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the ContactID of the given ContactUpdateRequest does not exist.")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given ContactUpdateRequest is invalid.")]
    public async Task<ActionResult<ContactResponseDto>> ContactPut([FromBody, SwaggerParameter("The Contact for which updates are to be persisted.", Required = true)] ContactUpdateRequestDto ContactUpdateRequest, CancellationToken cancellationToken)
    {
        log.Start();

        var contact = ContactUpdateRequest.ToContact();
        var result = await mediator.Send(new ContactUpdateCommand(contact), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return log.Finish(NotFound(ModelState));
        }

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return log.Finish(UnprocessableEntity(ModelState));
        }

        var response = result.Value.ToContactResponseDto();
        return log.Finish(Ok(response));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new Contact.", OperationId = "PostContact")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created Contact with its assigned newly ID.", type: typeof(ContactResponseDto))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given ContactCreateRequest is invalid.")]
    public async Task<ActionResult<ContactResponseDto>> ContactPost([FromBody, SwaggerParameter("The details of the Contact to be created.", Required = true)] ContactCreateRequestDto ContactCreateRequest, CancellationToken cancellationToken)
    {
        log.Start();

        var contact = ContactCreateRequest.ToContact();
        var result = await mediator.Send(new ContactCreateCommand(contact), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
        }

        var response = result.Value.ToContactResponseDto();
        return log.Finish(CreatedAtRoute(nameof(ContactGetById), new { response.ContactID }, response));
    }

    [HttpGet("{contactID}", Name = nameof(ContactGetById))]
    [SwaggerOperation("Gets the Contact with the given ID.", OperationId = "GetContactById")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK - returns the Contact requested.", type: typeof(ContactResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given ContactID does not exist.")]
    public async Task<ActionResult<ContactResponseDto>> ContactGetById([SwaggerParameter("The ID of the Contact to be returned.", Required = true)] int contactId, CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new ContactByIDQuery(contactId), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return log.Finish(NotFound());

        var response = result.Value.ToContactResponseDto();
        return log.Finish(Ok(response));
    }

    [HttpGet]
    [SwaggerOperation("Lists all contacts", OperationId = "GetContact")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of ContactResponseDto", type: typeof(IEnumerable<ContactResponseDto>))]
    public async Task<ActionResult<IEnumerable<ContactResponseDto>>> GetContact(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new ContactQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToContactResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
