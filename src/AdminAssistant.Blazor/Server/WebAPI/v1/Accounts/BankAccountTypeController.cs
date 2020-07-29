using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.Framework.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>Lists all bank account types supported by the API wherever a BankAccountTypeID can be provided.</summary>
        /// <returns>A list of BankAccountTypeResponseDto</returns>
        /// <response code="200">Ok</response>
        [HttpGet(Name = "GetBankAccountType")]
        [ProducesResponseType(typeof(IEnumerable<BankAccountTypeResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BankAccountTypeResponseDto>>> Get()
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new BankAccountTypesQuery()).ConfigureAwait(false);
            var response = this.Mapper.Map<IEnumerable<BankAccountTypeResponseDto>>(result.Value);

            return this.Log.Finish(this.Ok(response));
        }
    }
}
