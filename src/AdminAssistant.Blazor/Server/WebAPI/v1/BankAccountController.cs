using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.CQRS;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.WebAPI.v1
{
    public class BankAccountController : WebAPIControllerBase
    {
        public BankAccountController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        /// <summary>Updates and existing BankAccount.</summary>
        /// <param name="bankAccount">The BankAccount for which updates are to be persisted</param>
        /// <returns>The updated BankAccount</returns>
        /// <response code="200">Ok</response>
        [HttpPut]
        [Produces("application/json")] // Define MediaType limits
        [ProducesResponseType(typeof(BankAccount), StatusCodes.Status202Accepted)]
        public async Task<ActionResult<BankAccount>> Put([FromBody]BankAccount bankAccount)
        {
            this.Log.Start();

            throw new System.NotImplementedException();
            // TODO: Validate bankAccount and return BadRequest status code if invalid.

            // TODO: If bankAccount.BankAccountID != Constants.UnknownRecordID then check if exists in the DB and if not return NotFound status code

            // TODO: do we want to split save into update and create before delegating to Mediator and if not do we wrap all Mediator responses so it determines the HTTP response code rather than the WebAPI controller?

            //var result = await Mediator.Send(new SaveBankAccountCommand(bankAccount)).ConfigureAwait(false);

            // TODO: Handle validation and Update vs Insert - missing item to update.

            // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
            //if (result == Constants.UnknownRecordID)
            //    return this.Log.Finish(this.UnprocessableEntity());
            //else
            //    return this.Log.Finish(this.Ok(result));
        }

        /// <summary></summary>
        /// <param name="bankAccount"></param>
        /// <returns>The newly created BankAccount</returns>
        [HttpPost]
        [Produces("application/json")] // Define MediaType limits
        public async Task<ActionResult<BankAccount>> Post([FromBody]BankAccount bankAccount)
        {
            this.Log.Start();

            throw new System.NotImplementedException();
            // TODO: Validate bankAccount and return BadRequest status code if invalid.

            // TODO: If bankAccount.BankAccountID != Constants.UnknownRecordID then check if exists in the DB and if not return NotFound status code

            // TODO: do we want to split save into update and create before delegating to Mediator and if not do we wrap all Mediator responses so it determines the HTTP response code rather than the WebAPI controller?

            //var result = await Mediator.Send(new SaveBankAccountCommand(bankAccount)).ConfigureAwait(false);

            // TODO: Handle validation and Update vs Insert - missing item to update.

            // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
            //if (result == Constants.UnknownRecordID)
            //    return this.Log.Finish(this.UnprocessableEntity());
            //else
            //    return this.Log.Finish(this.Ok(result));
        }

        /// <summary>Returns a BankAccount with the given ID.</summary>
        /// <param name="bankAccountID">The ID of the BankAccount to be returned.</param>
        /// <returns>A BankAccount</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound - When the given <paramref name="bankAccountID"/> does not exist.</response>
        [HttpGet("{bankAccountID}")]
        [Produces("application/json")] // Define MediaType limits
        [ProducesResponseType(typeof(BankAccount), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BankAccount>> Get(int bankAccountID)
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new GetBankAccountByIDQuery(bankAccountID)).ConfigureAwait(false);

            if (result.Status == ResultStatus.NotFound)
                return this.Log.Finish(this.NotFound());
            else
                return this.Log.Finish(this.Ok(result.Value));
        }

        /// <summary>Returns the transactions since the last bank account statement for the BankAccount with the given ID.</summary>
        /// <param name="bankAccountID">The ID of the BankAccount.</param>
        /// <returns>A list of BankAccountTransaction</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound - When the given <paramref name="bankAccountID"/> does not exist.</response>
        [HttpGet("{bankAccountID}/transactions")] // Define MediaType limits
        [Produces("application/json")] // Define MediaType limits
        [ProducesResponseType(typeof(IEnumerable<BankAccount>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BankAccountTransaction>>> GetBankAccountTransactionListAsync(int bankAccountID)
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new GetBankAccountTransactionsByIDQuery(bankAccountID)).ConfigureAwait(false);

            if (result.Status == ResultStatus.NotFound)
                return this.Log.Finish(this.NotFound());
            else
                return this.Log.Finish(this.Ok(result));
        }
    }
}
