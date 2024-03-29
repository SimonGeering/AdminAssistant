using AdminAssistant.Shared;

namespace AdminAssistant.Modules.AssetRegisterModule;

public sealed record Asset : IPersistable
{
    public const int AssetNameMaxLength = EntityName.MaxLength;

    public AssetId AssetID { get; set; } = AssetId.Default;
    public string AssetName { get; set; } = string.Empty;

    public Id PrimaryKey => AssetID;
}
public sealed record AssetId(int Value) : Id(Value)
{
    public static AssetId Default => new(Constants.UnknownRecordID);
}
