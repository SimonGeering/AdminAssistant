using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AssetRegisterModule.CQRS;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AssetRegisterModule
{
    [ApiController]
    [Route("api/v1/assetregister-module/[controller]")]
    [ApiExplorerSettings(GroupName = "Asset Register Module")]
    public class AssetController : WebAPIControllerBase
    {
        public AssetController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
            : base(mapper, mediator, loggingProvider)
        {
        }

        [HttpGet]
        [SwaggerOperation("Lists all assets", OperationId = "GetAsset")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of AssetResponseDto", type: typeof(IEnumerable<AssetResponseDto>))]
        public async Task<ActionResult<IEnumerable<AssetResponseDto>>> GetAssets()
        {
            Log.Start();

            var result = await Mediator.Send(new AssetQuery()).ConfigureAwait(false);
            var response = Mapper.Map<IEnumerable<AssetResponseDto>>(result.Value);

            return Log.Finish(Ok(response));
        }
    }
}
