namespace AdminAssistant.Abstractions.DomainModel.Shared;

public abstract record EntityName(string Value)
{
    /// <summary>
    /// The maximum length of a name filed
    /// </summary>
    /// <remarks>
    /// For example the name of a person or company.
    /// </remarks>
    public const int MaxLength = 50;
}
