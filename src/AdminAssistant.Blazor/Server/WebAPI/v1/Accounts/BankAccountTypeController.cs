using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.Framework.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1
{
    [ApiController]
    [Route("api/v1/accounts/[controller]")]
    [ApiExplorerSettings(GroupName = "Accounts - BankAccountType")]
    public class BankAccountTypeController : WebAPIControllerBase
    {
        public BankAccountTypeController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BankAccountTypeResponseDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Lists all bank account types supported by the API wherever a BankAccountTypeID can be provided.", OperationId = "GetBankAccountType")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankAccountTypeResponseDto", type: typeof(IEnumerable<BankAccountTypeResponseDto>))]
        public async Task<ActionResult<IEnumerable<BankAccountTypeResponseDto>>> Get()
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new BankAccountTypesQuery()).ConfigureAwait(false);
            var response = this.Mapper.Map<IEnumerable<BankAccountTypeResponseDto>>(result.Value);

            return this.Log.Finish(this.Ok(response));
        }
    }
}
