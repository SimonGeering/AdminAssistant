namespace AdminAssistant.DomainModel.Modules.AssetRegisterModule.Builders;

public interface IAssetBuilder
{
    Asset Build();
    IAssetBuilder WithTestData(int assetID = Constants.UnknownRecordID);
    IAssetBuilder WithAssetName(string assetName);
}
