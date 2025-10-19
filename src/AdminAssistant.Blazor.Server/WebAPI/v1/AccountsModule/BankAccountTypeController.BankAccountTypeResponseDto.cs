namespace AdminAssistant.WebAPI.v1.AccountsModule;

public sealed record BankAccountTypeResponseDto
{
    public int BankAccountTypeID { get; init; }
    public string Description { get; init; } = string.Empty;
}
