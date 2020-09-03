using AdminAssistant.DomainModel.Modules.AssetRegisterModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.AssetRegisterModule
{
    public class AssetResponseDto : IMapFrom<Asset>
    {
        public int AssetID { get; set; }
        public string AssetName { get; set; } = string.Empty;
    }
}
