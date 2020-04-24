using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Blazor.Server.WebAPI.v1
{
    public class BankAccountController : WebAPIControllerBase
    {
        public BankAccountController(AutoMapper.IMapper mapper, MediatR.IMediator mediator)
            : base(mapper, mediator)
        {
        }

        /// <summary>
        /// Updates and existing BankAccount.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        [HttpPut("")]
        public async Task<ActionResult<BankAccount>> Put([FromBody]BankAccount bankAccount)
        {
            throw new System.NotImplementedException();
            // TODO: Validate bankAccount and return BadRequest status code if invalid.

            // TODO: If bankAccount.BankAccountID != Constants.UnknownRecordID then check if exists in the DB and if not return NotFound status code

            // TODO: do we want to split save into update and create before delegating to Mediator and if not do we wrap all Mediator responses so it determines the HTTP response code rather than the WebAPI controller?

            //var result = await Mediator.Send(new SaveBankAccountCommand(bankAccount)).ConfigureAwait(false);

            // TODO: Handle validation and Update vs Insert - missing item to update.

            // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
            //if (result == Constants.UnknownRecordID)
            //    return this.UnprocessableEntity();
            //else
            //    return this.Ok(result);
        }


        [HttpPost("")]
        public async Task<ActionResult<BankAccount>> Post([FromBody]BankAccount bankAccount)
        {
            throw new System.NotImplementedException();
            // TODO: Validate bankAccount and return BadRequest status code if invalid.

            // TODO: If bankAccount.BankAccountID != Constants.UnknownRecordID then check if exists in the DB and if not return NotFound status code

            // TODO: do we want to split save into update and create before delegating to Mediator and if not do we wrap all Mediator responses so it determines the HTTP response code rather than the WebAPI controller?

            //var result = await Mediator.Send(new SaveBankAccountCommand(bankAccount)).ConfigureAwait(false);

            // TODO: Handle validation and Update vs Insert - missing item to update.

            // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
            //if (result == Constants.UnknownRecordID)
            //    return this.UnprocessableEntity();
            //else
            //    return this.Ok(result);
        }

        /// <summary>
        /// Returns a BankAccount with the given ID.
        /// </summary>
        /// <param name="bankAccountID">The ID of the BankAccount to be returned.</param>
        /// <returns>A BankAccount</returns>
        [HttpGet("{bankAccountID}")]
        public async Task<ActionResult<BankAccount>> Get(int bankAccountID)
        {
            var result = await this.Mediator.Send(new GetBankAccountByIDQuery(bankAccountID)).ConfigureAwait(false);

            if (result == null)
                return this.NotFound();
            else
                return this.Ok(result);
        }

        /// <summary>
        /// Returns the transactions since the last bank account statement for the BankAccount with the given ID.
        /// </summary>
        /// <param name="bankAccountID">The ID of the BankAccount.</param>
        /// <returns>A list of BankAccountTransaction</returns>
        [HttpGet("{bankAccountID}/transactions")]
        public async Task<ActionResult<IEnumerable<BankAccountTransaction>>> GetBankAccountTransactionListAsync(int bankAccountID)
        {
            var result = await this.Mediator.Send(new GetBankAccountTransactionsByIDQuery(bankAccountID)).ConfigureAwait(false);

            if (result == null)
                return this.NotFound();
            else
                return this.Ok(result);
        }
    }
}
