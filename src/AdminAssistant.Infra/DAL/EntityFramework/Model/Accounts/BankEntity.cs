using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;

public sealed class BankEntity : IMapFrom<Bank>, IMapTo<Bank>
{
    public int BankID { get; set; }
    public string BankName { get; set; } = string.Empty;
}
