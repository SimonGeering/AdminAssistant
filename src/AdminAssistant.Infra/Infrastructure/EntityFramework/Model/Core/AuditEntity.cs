using AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;
using AdminAssistant.Infrastructure.EntityFramework.Model.AssetRegister;
using AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;

namespace AdminAssistant.Infrastructure.EntityFramework.Model.Core;

public sealed class AuditEntity
{
    public int AuditID { get; set; }
    public bool IsArchived { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime ArchivedOn { get; set; }
    public string ArchivedBy { get; set; } = string.Empty;
    public DateTime DeletedOn { get; set; }
    public string DeletedBy { get; set; } = string.Empty;

    public AddressEntity Address { get; internal set; } = null!;
    public AssetEntity Asset { get; internal set; } = null!;
    public CompanyEntity Company { get; internal set; } = null!;
    public PersonalDetailsEntity PersonalDetails { get; internal set; } = null!;
    public UserProfileEntity UserProfile { get; internal set; } = null!;
    public BankAccountEntity BankAccount { get; internal set; } = null!;
}
