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
    public class CurrencyController : WebAPIControllerBase
    {
        public CurrencyController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        /// <summary>Lists all currencies supported by the API wherever a CurrencyID can be provided.</summary>
        /// <returns>A list of CurrencyResponseDto</returns>
        /// <response code="200">Ok</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CurrencyResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CurrencyResponseDto>>> Get()
        {
            this.Log.Start();

            var result = await Mediator.Send(new CurrenciesQuery()).ConfigureAwait(false);
            var response = this.Mapper.Map<IEnumerable<CurrencyResponseDto>>(result.Value);

            return this.Log.Finish(this.Ok(response));
        }
    }
}
