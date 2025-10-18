using AdminAssistant.Modules.AssetRegisterModule;

namespace AdminAssistant.WebAPI.v1.AssetRegisterModule;

public static class BudgetModuleMapper
{
    public static IEnumerable<AssetResponseDto> ToBudgetResponseDtoEnumeration(this IEnumerable<Asset> source)
        => source.Select(x => new AssetResponseDto
        {
            AssetID = x.AssetID.Value,
            AssetName = x.AssetName
        });
}
