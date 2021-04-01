using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.AssetRegisterModule
{
    public record Asset : IDatabasePersistable
    {
        public const int AssetNameMaxLength = Constants.NameMaxLength;

        public int AssetID { get; set; }
        public string AssetName { get; set; } = string.Empty;

        public int PrimaryKey => AssetID;
    }
}
