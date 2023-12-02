using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule.Queries;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[ApiController]
[Route("api/v1/accounts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Accounts Module")]
public sealed class BankAccountController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpPut]
    [SwaggerOperation("Update an existing BankAccount.", OperationId = "PutBankAccount")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated BankAccountResponseDto", type: typeof(BankAccountResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the BankAccountID of the given bankAccountUpdateRequest does not exist.")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given bankAccountUpdateRequest is invalid.")]
    public async Task<ActionResult<BankAccountResponseDto>> BankAccountPut([FromBody, SwaggerParameter("The BankAccount for which updates are to be persisted.", Required = true)] BankAccountUpdateRequestDto bankAccountUpdateRequest, CancellationToken cancellationToken)
    {
        Log.Start();

        var bankAccount = Mapper.Map<BankAccount>(bankAccountUpdateRequest);
        var result = await Mediator.Send(new BankAccountUpdateCommand(bankAccount), cancellationToken).ConfigureAwait(false);

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

        var response = Mapper.Map<BankAccountResponseDto>(result.Value);
        return Log.Finish(Ok(response));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new BankAccount.", OperationId = "PostBankAccount")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created bank account with its assigned newly ID.", type: typeof(BankAccountResponseDto))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given bankAccountCreateRequest is invalid.")]
    public async Task<ActionResult<BankAccountResponseDto>> BankAccountPost([FromBody, SwaggerParameter("The details of the BankAccount to be created.", Required = true)] BankAccountCreateRequestDto bankAccountCreateRequest, CancellationToken cancellationToken)
    {
        Log.Start();

        var bankAccount = Mapper.Map<BankAccount>(bankAccountCreateRequest);
        var result = await Mediator.Send(new BankAccountCreateCommand(bankAccount), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return Log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
        }

        var response = Mapper.Map<BankAccountResponseDto>(result.Value);
        return Log.Finish(CreatedAtRoute(nameof(BankAccountGetById), new { bankAccountID = response.BankAccountID }, response));
    }

    [HttpGet("{bankAccountID}")]
    [SwaggerOperation("Gets the BankAccountResponseDto with the given ID.", OperationId = "GetBankAccountById")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK - returns the BankAccount requested.", type: typeof(BankAccountResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given BankAccountID does not exist.")]
    public async Task<ActionResult<BankAccountResponseDto>> BankAccountGetById([SwaggerParameter("The ID of the BankAccount to be returned.", Required = true)] int bankAccountID, CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new BankAccountByIDQuery(bankAccountID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return Log.Finish(NotFound());

        var response = Mapper.Map<BankAccountResponseDto>(result.Value);
        return Log.Finish(Ok(response));
    }

    [HttpGet("{bankAccountID}/transactions")]
    [SwaggerOperation("Get the transactions since the last bank account statement for the BankAccount with the given ID.", OperationId = "GetBankAccountTransactionByBankAccountID")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK - returns a list of BankAccountTransactionResponseDto.", type: typeof(IEnumerable<BankAccountTransactionResponseDto>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given BankAccountID does not exist.")]
    public async Task<ActionResult<IEnumerable<BankAccountTransactionResponseDto>>> BankAccountTransactionsGetByBankAccountID([SwaggerParameter("The ID of the BankAccount.", Required = true)] int bankAccountID, CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new BankAccountTransactionsByBankAccountIDQuery(bankAccountID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return Log.Finish(NotFound());

        var response = Mapper.Map<IEnumerable<BankAccountTransactionResponseDto>>(result.Value);
        return Log.Finish(Ok(response));
    }
}
