using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.Framework.Providers;
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

        [HttpGet]
        [SwaggerOperation("Lists all banks.", OperationId = "GetBank")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankResponseDto", type: typeof(IEnumerable<BankResponseDto>))]
        public async Task<ActionResult<IEnumerable<BankResponseDto>>> Get()
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new BankQuery()).ConfigureAwait(false);
            var response = this.Mapper.Map<IEnumerable<BankResponseDto>>(result.Value);
            
            return this.Log.Finish(this.Ok(response));
        }
    }
}
