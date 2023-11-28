namespace AdminAssistant.Framework.Primitives;

public abstract record Id(int Value)
{
    public bool IsUnknownRecordID => Value == Constants.UnknownRecordID;

    /// <summary>
    /// Flag to determine if the entity is newly created in memory or if it has been previously persisted to the database.
    /// </summary>
    public bool IsNewRecordID => Value == Constants.NewRecordID;
}
