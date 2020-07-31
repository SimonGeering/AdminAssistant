using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.Framework.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>Lists all banks.</summary>
        /// <returns>A list of BankResponseDto</returns>
        /// <response code="200">Ok</response>
        [HttpGet(Name = "GetBank")]
        [ProducesResponseType(typeof(IEnumerable<BankResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BankResponseDto>>> Get()
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new BankQuery()).ConfigureAwait(false);
            var response = this.Mapper.Map<IEnumerable<BankResponseDto>>(result.Value);
            
            return this.Log.Finish(this.Ok(response));
        }
    }
}
