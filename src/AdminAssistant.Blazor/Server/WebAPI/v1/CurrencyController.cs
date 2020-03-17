using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Blazor.Server.WebAPI.v1
{
    public class CurrencyController : WebAPIControllerBase
    {
        public CurrencyController(AutoMapper.IMapper mapper, MediatR.IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> Get()
        {
            var result = await Mediator.Send(new GetCurrenciesQuery()).ConfigureAwait(false);
            return this.Ok(result);
        }
    }
}