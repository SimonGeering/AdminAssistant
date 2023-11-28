namespace AdminAssistant.Abstractions.DomainModel.Shared;

public abstract record EntityDescription(string Value)
{
    /// <summary>
    /// The maximum length of a description field.
    /// </summary>
    public const int MaxLength = 255;
}
