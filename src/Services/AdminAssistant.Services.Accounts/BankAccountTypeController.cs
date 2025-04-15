// using AdminAssistant.Modules.AccountsModule.Queries;
//
// namespace AdminAssistant.WebAPI.v1.AccountsModule;
//
// [ApiController]
// [Route("api/v1/accounts-module/[controller]")]
// [ApiExplorerSettings(GroupName = "Accounts Module")]
// public sealed class BankAccountTypeController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
//     : WebApiControllerBase(mapper, mediator, loggingProvider)
// {
//     [HttpGet]
//     [SwaggerOperation("Lists all bank account types supported by the API wherever a BankAccountTypeID can be provided.", OperationId = "GetBankAccountType")]
//     [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankAccountTypeResponseDto", type: typeof(IEnumerable<BankAccountTypeResponseDto>))]
//     public async Task<ActionResult<IEnumerable<BankAccountTypeResponseDto>>> BankAccountTypeGet(CancellationToken cancellationToken)
//     {
//         Log.Start();
//
//         var result = await Mediator.Send(new BankAccountTypesQuery(), cancellationToken).ConfigureAwait(false);
//         var response = Mapper.Map<IEnumerable<BankAccountTypeResponseDto>>(result.Value);
//
//         return Log.Finish(Ok(response));
//     }
// }
