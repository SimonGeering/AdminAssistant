using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.CQRS;
using AdminAssistant.Framework.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Blazor.Server.WebAPI.v1
{
    public class BankAccountInfoController : WebAPIControllerBase
    {
        public BankAccountInfoController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        /// <summary>Returns a list of BankAccountInfo summarizing the available BankAccounts owned by the logged in user.</summary>
        /// <returns>A collection of BankAccountInfo</returns>
        /// <response code="200">Ok - When one or more BankAccountInfo items are returned</response>
        [HttpGet]
        [Produces("application / json")] // Define MediaType limits
        [ProducesResponseType(typeof(IEnumerable<BankAccountInfo>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BankAccountInfo>>> Get()
        {
            this.Log.Start();

            // TODO: Hard coded user ID.
            var result = await Mediator.Send(new GetBankAccountInfoQuery(10)).ConfigureAwait(false);

            return this.Log.Finish(this.Ok(result.Value));
        }
    }
}
