namespace AdminAssistant.DomainModel.Modules.AssetRegisterModule.Builders;

internal class AssetBuilder : IAssetBuilder
{
    private Asset _asset = new();

    public static Asset Default(IAssetBuilder builder) => builder.Build();
    public static Asset Default(AssetBuilder builder) => builder.Build();

    public Asset Build() => _asset;

    public IAssetBuilder WithTestData(int assetID = Constants.UnknownRecordID)
    {
        _asset = _asset with
        {
            AssetID = assetID,
            AssetName = "2 bed detached house"
        };
        return this;
    }
    public IAssetBuilder WithAssetName(string assetName)
    {
        _asset = _asset with { AssetName = assetName };
        return this;
    }
}
