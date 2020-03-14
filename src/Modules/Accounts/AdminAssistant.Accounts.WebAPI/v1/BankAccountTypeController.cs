using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.Accounts.DomainModel;
using AdminAssistant.Accounts.DomainModel.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Accounts.WebAPI.v1
{
    public class BankAccountTypeController : WebAPIControllerBase
    {
        public BankAccountTypeController(AutoMapper.IMapper mapper, MediatR.IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccountType>>> Get()
        {
            var result = await Mediator.Send(new GetBankAccountTypesQuery()).ConfigureAwait(false);
            return this.Ok(result);
        }
    }
}
