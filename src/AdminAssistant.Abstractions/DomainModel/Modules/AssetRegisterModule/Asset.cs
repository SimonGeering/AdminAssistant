namespace AdminAssistant.DomainModel.Modules.AssetRegisterModule;

public sealed record Asset : IDatabasePersistable
{
    public const int AssetNameMaxLength = Constants.NameMaxLength;

    public AssetId AssetID { get; set; } = AssetId.Default;
    public string AssetName { get; set; } = string.Empty;

    public Id PrimaryKey => AssetID;
}
public sealed record AssetId(int Value) : Id(Value)
{
    public static AssetId Default => new(Constants.UnknownRecordID);
}
