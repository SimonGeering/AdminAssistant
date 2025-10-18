namespace AdminAssistant.WebAPI.v1.AssetRegisterModule;

public sealed record AssetResponseDto
{
    public int AssetID { get; init; }
    public string AssetName { get; init; } = string.Empty;
}
