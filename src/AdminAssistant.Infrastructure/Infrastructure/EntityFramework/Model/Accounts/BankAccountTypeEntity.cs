namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class BankAccountTypeEntity
{
    public int BankAccountTypeID { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool AllowPersonal { get; set; }
    public bool AllowCompany { get; set; }
    public bool IsDeprecated { get; set; }
}
