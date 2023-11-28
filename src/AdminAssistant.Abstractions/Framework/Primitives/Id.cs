namespace AdminAssistant.Framework.Primitives;

public abstract record Id(int Value)
{
    public bool IsUnknownRecordID => Value == Constants.UnknownRecordID;
    public bool IsNewRecordID => Value == Constants.NewRecordID;
}
