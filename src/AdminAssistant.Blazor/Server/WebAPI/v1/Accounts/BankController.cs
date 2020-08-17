using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1
{
    [ApiController]
    [Route("api/v1/accounts/[controller]")]
    [ApiExplorerSettings(GroupName = "Accounts - Bank")]
    public class BankController : WebAPIControllerBase
    {
        public BankController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        [HttpGet("{bankID}")]
        [SwaggerOperation("Gets the Bank with the given ID.", OperationId = "GetBankById")]
        [SwaggerResponse(StatusCodes.Status200OK, "OK - returns the Bank requested.", type: typeof(BankResponseDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given BankID does not exist.")]
        public async Task<ActionResult<BankResponseDto>> BankGetById([SwaggerParameter("The ID of the Bank to be returned.", Required = true)] int bankID)
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new BankByIDQuery(bankID)).ConfigureAwait(false);

            if (result.Status == ResultStatus.NotFound)
                return this.Log.Finish(this.NotFound());

            var response = this.Mapper.Map<BankResponseDto>(result.Value);
            return this.Log.Finish(this.Ok(response));
        }

        [HttpGet]
        [SwaggerOperation("Lists all banks.", OperationId = "GetBank")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankResponseDto", type: typeof(IEnumerable<BankResponseDto>))]
        public async Task<ActionResult<IEnumerable<BankResponseDto>>> BankGet()
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new BankQuery()).ConfigureAwait(false);
            var response = this.Mapper.Map<IEnumerable<BankResponseDto>>(result.Value);
            
            return this.Log.Finish(this.Ok(response));
        }
    }
}
