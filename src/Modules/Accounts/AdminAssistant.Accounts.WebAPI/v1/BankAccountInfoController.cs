using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.Accounts.DomainModel;
using AdminAssistant.Accounts.DomainModel.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Accounts.WebAPI.v1
{
    public class BankAccountInfoController : WebAPIControllerBase
    {
        public BankAccountInfoController(AutoMapper.IMapper mapper, MediatR.IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet("{ownerID}")]
        public async Task<ActionResult<IEnumerable<BankAccountInfo>>> Get()
        {
            var result = await Mediator.Send(new GetBankAccountInfoQuery()).ConfigureAwait(false);
            return this.Ok(result);
        }
    }
}