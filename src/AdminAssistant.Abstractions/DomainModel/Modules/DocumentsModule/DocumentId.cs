namespace AdminAssistant.DomainModel.Modules.DocumentsModule;

public sealed record DocumentId(int Value) : Id(Value)
{
    public static DocumentId Default => new(Constants.UnknownRecordID);
}
