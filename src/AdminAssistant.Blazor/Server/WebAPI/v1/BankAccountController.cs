using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.Accounts.DomainModel;
using AdminAssistant.Accounts.DomainModel.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Accounts.WebAPI.v1
{
    public class BankAccountController : WebAPIControllerBase
    {
        public BankAccountController(AutoMapper.IMapper mapper, MediatR.IMediator mediator)
            : base(mapper, mediator)
        {
        }

        [HttpGet("{bankAccountID}")]
        public async Task<ActionResult<BankAccount>> Get(int bankAccountID)
        {
            var result = await Mediator.Send(new GetBankAccountByIDQuery(bankAccountID)).ConfigureAwait(false);

            if (result == null)
                return this.NotFound();
            else
                return this.Ok(result);
        }

        [HttpGet("{bankAccountID}/transactions")]
        public async Task<ActionResult<IEnumerable<BankAccountTransaction>>> GetBankAccountTransactionListAsync(int bankAccountID)
        {
            var result = await Mediator.Send(new GetBankAccountTransactionsByIDQuery(bankAccountID)).ConfigureAwait(false);

            if (result == null)
                return this.NotFound();
            else
                return this.Ok(result);
        }
    }
}
