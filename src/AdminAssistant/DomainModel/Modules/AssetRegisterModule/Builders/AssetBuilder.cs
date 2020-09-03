namespace AdminAssistant.DomainModel.Modules.AssetRegisterModule.Builders
{
    public interface IAssetBuilder
    {
        Asset Build();
        IAssetBuilder WithTestData(int assetID = Constants.UnknownRecordID);
        IAssetBuilder WithAssetName(string assetName);
    }
    internal class AssetBuilder : Asset, IAssetBuilder
    {
        public static Asset Default(IAssetBuilder builder) => builder.Build();
        public static Asset Default(AssetBuilder builder) => builder.Build();

        public Asset Build() => this;

        public IAssetBuilder WithTestData(int assetID = Constants.UnknownRecordID)
        {
            AssetID = assetID;
            AssetName = "2 bed detached house";
            return this;
        }
        public IAssetBuilder WithAssetName(string assetName)
        {
            AssetName = assetName;
            return this;
        }
    }
}