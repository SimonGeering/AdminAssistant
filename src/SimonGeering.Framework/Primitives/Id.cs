namespace SimonGeering.Framework.Primitives;

public abstract record Id(int Value)
{
    public bool IsUnknownRecordId => Value == Constants.UnknownRecordId;

    /// <summary>
    /// Flag to determine if the entity is newly created in memory or if it has been previously persisted to the database.
    /// </summary>
    public bool IsNewRecordId => Value == Constants.NewRecordId;
}
