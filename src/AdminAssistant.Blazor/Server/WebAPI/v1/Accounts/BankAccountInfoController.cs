using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Infrastructure;
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
    [ApiExplorerSettings(GroupName = "Accounts - BankAccountInfo")]
    public class BankAccountInfoController : WebAPIControllerBase
    {
        private readonly IUserContextProvider userContextProvider;

        public BankAccountInfoController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider, IUserContextProvider userContextProvider)
            : base(mapper, mediator, loggingProvider)
        {
            this.userContextProvider = userContextProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BankAccountInfoResponseDto>), StatusCodes.Status200OK)]
        [SwaggerOperation("Lists the summary info for all the available BankAccounts owned by the logged in user.", OperationId = "GetBankAccountInfo")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankAccountInfoResponseDto", type: typeof(IEnumerable<BankAccountInfoResponseDto>))]
        public async Task<ActionResult<IEnumerable<BankAccountInfoResponseDto>>> Get()
        {
            this.Log.Start();
            
            var result = await this.Mediator.Send(new BankAccountInfoQuery(this.userContextProvider.GetCurrentUser().UserID)).ConfigureAwait(false);
            var response = this.Mapper.Map<IEnumerable<BankAccountInfoResponseDto>>(result.Value);

            return this.Log.Finish(this.Ok(response));
        }
    }
}
