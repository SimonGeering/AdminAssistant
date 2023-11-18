using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule;

public interface IAccountsService
{
    Task<List<BankAccountType>> LoadBankAccountTypesLookupDataAsync();
    Task<BankAccount> CreateBankAccountAsync(BankAccount model);
    Task<BankAccount> UpdateBankAccountAsync(BankAccount model);
    Task<IEnumerable<string>> ParseBankAccountStatementAsync(byte[] fileContent);
}