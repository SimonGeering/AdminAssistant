namespace AdminAssistant.WebAPI.v1.AccountsModule;

public sealed record BankResponseDto
{
    public int BankID { get; init; } = Constants.UnknownRecordID;
    public string BankName { get; init; } = string.Empty;
}
