namespace AdminAssistant.Infra.DAL;

public interface IDatabasePersistable
{
    int PrimaryKey { get; }
    bool IsNew => PrimaryKey == Constants.NewRecordID;
}
