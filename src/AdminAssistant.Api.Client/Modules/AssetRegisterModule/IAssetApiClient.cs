using AdminAssistant.WebAPI.v1.AssetRegisterModule;

namespace AdminAssistant.WebAPIClient.v1.AssetRegisterModule;

public interface IAssetApiClient
{
    [Get("/api/v1/assetregister-module/Asset")]
    Task<IEnumerable<AssetResponseDto>> GetAssetsAsync(CancellationToken cancellationToken = default);
}
