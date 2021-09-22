using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;

public class BankEntity : IMapFrom<Bank>, IMapTo<Bank>
{
    public int BankID { get; set; }
    public string BankName { get; set; } = string.Empty;
}
