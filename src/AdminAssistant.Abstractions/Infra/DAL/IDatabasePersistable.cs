namespace AdminAssistant.Infra.DAL;

/// <summary>
/// Allows implementing class to be persisted to a relational database. 
/// </summary>
public interface IDatabasePersistable
{
    /// <summary>
    /// The Unique Identifier for the entity within its coresponding database table.
    /// </summary>
    Id PrimaryKey { get; }

    /// <summary>
    /// Flag to determine if the entity is newly created in memory or if it has been previously persisted to the database.
    /// </summary>
    bool IsNew => PrimaryKey.IsNewRecordID;
}
