namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class BankEntity
{
    public int BankID { get; set; }
    public string BankName { get; set; } = string.Empty;
}
