using AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

namespace AdminAssistant.Infrastructure.EntityFramework.Model.Core;

public sealed class OwnerEntity
{
    public int OwnerID { get; set; }
    public int? CompanyID { get; set; }
    public int? PersonalDetailsID { get; set; }

    public CompanyEntity Company { get; set; } = null!;
    public PersonalDetailsEntity PersonalDetails { get; set; } = null!;
    public IList<BankAccountEntity> BankAccounts { get; internal set; } = new List<BankAccountEntity>();
}
