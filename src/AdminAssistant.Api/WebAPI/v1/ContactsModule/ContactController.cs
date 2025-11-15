using AdminAssistant.Modules.ContactsModule.Commands;
using AdminAssistant.Modules.ContactsModule.Queries;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

[ApiController]
[Route("api/v1/contacts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Contacts Module")]
public sealed class ContactController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Update an existing Contact.
    /// </summary>
    /// <param name="ContactUpdateRequest">The Contact for which updates are to be persisted.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated <see cref="ContactResponseDto"/>.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<ContactResponseDto>> ContactPut(
        [FromBody] ContactUpdateRequestDto ContactUpdateRequest,
        CancellationToken cancellationToken)
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

    /// <summary>
    /// Creates a new Contact.
    /// </summary>
    /// <param name="ContactCreateRequest">The details of the Contact to be created.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created <see cref="ContactResponseDto"/> with its newly assigned ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<ContactResponseDto>> ContactPost(
        [FromBody] ContactCreateRequestDto ContactCreateRequest,
        CancellationToken cancellationToken)
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

    /// <summary>
    /// Gets the Contact with the given ID.
    /// </summary>
    /// <param name="contactId">The ID of the Contact to be returned.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The requested <see cref="ContactResponseDto"/>.</returns>
    [HttpGet("{contactID}", Name = nameof(ContactGetById))]
    [ProducesResponseType(typeof(ContactResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactResponseDto>> ContactGetById(
        int contactId,
        CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new ContactByIDQuery(contactId), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return log.Finish(NotFound());

        var response = result.Value.ToContactResponseDto();
        return log.Finish(Ok(response));
    }

    /// <summary>
    /// Lists all contacts.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="ContactResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContactResponseDto>>> GetContact(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new ContactQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToContactResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
