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
    [Route("api/v1/core/[controller]")]
    [ApiExplorerSettings(GroupName = "Core - Currency")]
    public class CurrencyController : WebAPIControllerBase
    {
        public CurrencyController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        [HttpGet]
        [SwaggerOperation("Lists all currencies supported by the API wherever a CurrencyID can be provided.", OperationId = "GetCurrency")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of CurrencyResponseDto", type: typeof(IEnumerable<CurrencyResponseDto>))]
        public async Task<ActionResult<IEnumerable<CurrencyResponseDto>>> Get()
        {
            this.Log.Start();

            var result = await this.Mediator.Send(new CurrenciesQuery()).ConfigureAwait(false);
            var response = this.Mapper.Map<IEnumerable<CurrencyResponseDto>>(result.Value);

            return this.Log.Finish(this.Ok(response));
        }
    }
}
