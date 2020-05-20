using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.DomainModel
{
    public interface IDatabasePersistable
    {
        int PrimaryKey { get; }
        bool IsNew => (this.PrimaryKey == Constants.NewRecordID);
    }
}
