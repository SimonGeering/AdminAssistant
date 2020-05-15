using System.Collections.Generic;
using System.Linq;
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
        [ProducesResponseType(typeof(BankAccount), StatusCodes.Status202Accepted)]
        public async Task<ActionResult<BankAccount>> Put([FromBody]BankAccountCreateRequestDto bankAccountRequest)
        {
            this.Log.Start();

            throw new System.NotImplementedException();
            // TODO: Validate bankAccount and return BadRequest status code if invalid.

            // TODO: If bankAccount.BankAccountID != Constants.UnknownRecordID then check if exists in the DB and if not return NotFound status code

            //var result = await Mediator.Send(new SaveBankAccountCommand(bankAccount)).ConfigureAwait(false);

            // TODO: Handle validation and Update vs Insert - missing item to update.

            // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
            //if (result == Constants.UnknownRecordID)
            //    return this.Log.Finish(this.UnprocessableEntity());
            //else
            //    return this.Log.Finish(this.Ok(result));
        }

        /// <summary>Creates a new BankAccount</summary>
        /// <param name="bankAccountCreateRequest">The details of the BankAccount to be created</param>
        /// <returns>A BankAccountResponseDto for the newly created BankAccount</returns>
        /// <response code="201">Created - When the bank account was created ok.</response>
        /// <response code="422">UnprocessableEntity - When the given <paramref name="bankAccountCreateRequest"/> is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(BankAccountResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<BankAccountResponseDto>> Post([FromBody]BankAccountCreateRequestDto bankAccountCreateRequest)
        {
            this.Log.Start();

            var bankAccount = Mapper.Map<BankAccount>(bankAccountCreateRequest);
            var result = await this.Mediator.Send(new BankAccountCreateCommand(bankAccount)).ConfigureAwait(false);

            if (result.Status == ResultStatus.Invalid)
            {
                result.ValidationErrors.ToList().ForEach((err) => this.ModelState.AddModelError(err.Key, err.Value));
                return this.Log.Finish(this.UnprocessableEntity(this.ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
            }

            var response = this.Mapper.Map<BankAccountResponseDto>(result.Value);
            return this.Log.Finish(this.CreatedAtRoute(nameof(GetBankAccountById), new {bankAccountID = response.BankAccountID}, response));
        }

        /// <summary>Returns a BankAccountResponseDto with the given ID.</summary>
        /// <param name="bankAccountID">The ID of the BankAccount to be returned.</param>
        /// <returns>A BankAccountResponseDto</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound - When the given <paramref name="bankAccountID"/> does not exist.</response>
        [HttpGet("{bankAccountID}", Name = "GetBankAccountById")]
        [ProducesResponseType(typeof(BankAccountResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BankAccountResponseDto>> GetBankAccountById(int bankAccountID)
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new GetBankAccountByIDQuery(bankAccountID)).ConfigureAwait(false);

            if (result.Status == ResultStatus.NotFound)
                return this.Log.Finish(this.NotFound());

            var response = this.Mapper.Map<BankAccountResponseDto>(result.Value);
            return this.Log.Finish(this.Ok(response));
        }

        /// <summary>Returns the transactions since the last bank account statement for the BankAccount with the given ID.</summary>
        /// <param name="bankAccountID">The ID of the BankAccount.</param>
        /// <returns>A list of BankAccountTransaction</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound - When the given <paramref name="bankAccountID"/> does not exist.</response>
        [HttpGet("{bankAccountID}/transactions")] // Define MediaType limits
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
