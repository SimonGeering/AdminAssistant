using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.CQRS;
using AdminAssistant.Framework.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Blazor.Server.WebAPI.v1
{
    public class BankAccountInfoController : WebAPIControllerBase
    {
        public BankAccountInfoController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        [HttpGet("{ownerID}")]
        public async Task<ActionResult<IEnumerable<BankAccountInfo>>> Get()
        {
            this.Log.Start();

            var result = await Mediator.Send(new GetBankAccountInfoQuery()).ConfigureAwait(false);

            return this.Log.Finish(this.Ok(result));
        }
    }
}
