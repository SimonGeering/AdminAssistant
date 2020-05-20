using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Infrastructure;
using AdminAssistant.DomainModel.Modules.Accounts.CQRS;
using AdminAssistant.Framework.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.WebAPI.v1
{
    public class BankAccountInfoController : WebAPIControllerBase
    {
        private readonly IUserContextProvider userContextProvider;

        public BankAccountInfoController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider, IUserContextProvider userContextProvider)
            : base(mapper, mediator, loggingProvider)
        {
            this.userContextProvider = userContextProvider;
        }

        /// <summary>Returns the summary info for all the available BankAccounts owned by the logged in user.</summary>
        /// <returns>A collection of BankAccountInfoResponseDto</returns>
        /// <response code="200">Ok - When one or more BankAccountInfo items are returned</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BankAccountInfoResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BankAccountInfoResponseDto>>> Get()
        {
            this.Log.Start();
            
            var result = await Mediator.Send(new BankAccountInfoQuery(this.userContextProvider.GetCurrentUser().UserID)).ConfigureAwait(false);
            var response = Mapper.Map<IEnumerable<BankAccountInfoResponseDto>>(result.Value);

            return this.Log.Finish(this.Ok(response));
        }
    }
}
