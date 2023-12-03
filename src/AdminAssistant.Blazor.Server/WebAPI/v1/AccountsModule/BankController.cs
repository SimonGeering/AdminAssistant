using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[ApiController]
[Route("api/v1/accounts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Accounts Module")]
public sealed class BankController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpPut]
    [SwaggerOperation("Update an existing Bank.", OperationId = "PutBank")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated BankResponseDto", type: typeof(BankResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the BankID of the given bankUpdateRequest does not exist.")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given bankUpdateRequest is invalid.")]
    public async Task<ActionResult<BankResponseDto>> BankPut([FromBody, SwaggerParameter("The Bank for which updates are to be persisted.", Required = true)] BankUpdateRequestDto bankUpdateRequest, CancellationToken cancellationToken)
    {
        Log.Start();

        var bank = Mapper.Map<Bank>(bankUpdateRequest);
        var result = await Mediator.Send(new BankUpdateCommand(bank), cancellationToken).ConfigureAwait(false);

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

        var response = Mapper.Map<BankResponseDto>(result.Value);
        return Log.Finish(Ok(response));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new Bank.", OperationId = "PostBank")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created bank with its assigned newly ID.", type: typeof(BankResponseDto))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given bankCreateRequest is invalid.")]
    public async Task<ActionResult<BankResponseDto>> BankPost([FromBody, SwaggerParameter("The details of the Bank to be created.", Required = true)] BankCreateRequestDto bankCreateRequest, CancellationToken cancellationToken)
    {
        Log.Start();

        var bank = Mapper.Map<Bank>(bankCreateRequest);
        var result = await Mediator.Send(new BankCreateCommand(bank), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return Log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
        }

        var response = Mapper.Map<BankResponseDto>(result.Value);
        return Log.Finish(CreatedAtRoute(nameof(BankGetById), new { bankID = response.BankID }, response));
    }

    [HttpGet("{bankID}", Name = nameof(BankGetById))]
    [SwaggerOperation("Gets the Bank with the given ID.", OperationId = "GetBankById")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK - returns the Bank requested.", type: typeof(BankResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given BankID does not exist.")]
    public async Task<ActionResult<BankResponseDto>> BankGetById([SwaggerParameter("The ID of the Bank to be returned.", Required = true)] int bankID, CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new BankByIDQuery(bankID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return Log.Finish(NotFound());

        var response = Mapper.Map<BankResponseDto>(result.Value);
        return Log.Finish(Ok(response));
    }

    [HttpGet]
    [SwaggerOperation("Lists all banks.", OperationId = "GetBank")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankResponseDto", type: typeof(IEnumerable<BankResponseDto>))]
    public async Task<ActionResult<IEnumerable<BankResponseDto>>> BankGet(CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new BankQuery(), cancellationToken).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<BankResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
