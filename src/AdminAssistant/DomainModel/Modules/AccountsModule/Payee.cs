namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record Payee
{
    public const int NameMaxLength = Constants.NameMaxLength;

    public int PayeeID { get; init; }
    public string Name { get; init; } = string.Empty;
}
