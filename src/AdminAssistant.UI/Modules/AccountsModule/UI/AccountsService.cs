using AdminAssistant.WebAPIClient.v1.AccountsModule;

namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IAccountsService
{
    Task<List<BankAccountType>> LoadBankAccountTypesLookupDataAsync();
    Task<BankAccount> CreateBankAccountAsync(BankAccount model);
    Task<BankAccount> UpdateBankAccountAsync(BankAccount model);
    Task<IEnumerable<string>> ParseBankAccountStatementAsync(byte[] fileContent);
}
internal sealed class AccountsService(
    IPdfFileProvider pdfFileProvider,
    IBankAccountTypeApiClient bankAccountTypeApiClient,
    IBankAccountApiClient bankAccountApiClient,
    ILoggingProvider log)
    : ServiceBase(log), IAccountsService
{
    public async Task<List<BankAccountType>> LoadBankAccountTypesLookupDataAsync()
    {
        Log.Start();

        var response = await bankAccountTypeApiClient.GetBankAccountTypesAsync().ConfigureAwait(false);

        var result = new List<BankAccountType>(response.ToBankAccountTypeList());
        result.Insert(0, new BankAccountType() { BankAccountTypeID = BankAccountTypeId.Default, Description = string.Empty });

        return Log.Finish(result);
    }

    public async Task<BankAccount> CreateBankAccountAsync(BankAccount model)
    {
        Log.Start();

        var request = model.ToBankAccountCreateRequestDto();

        var response = await bankAccountApiClient.CreateBankAccountAsync(request).ConfigureAwait(false);

        var result = response.ToBankAccount();
        return Log.Finish(result);
    }

    public async Task<BankAccount> UpdateBankAccountAsync(BankAccount model)
    {
        Log.Start();

        var request = model.ToBankAccountUpdateRequestDto();

        var response = await bankAccountApiClient.UpdateBankAccountAsync(request).ConfigureAwait(false);

        var result = response.ToBankAccount();
        return Log.Finish(result);
    }

    public async Task<IEnumerable<string>> ParseBankAccountStatementAsync(byte[] fileContent)
    {
        Log.Start();

        var result = await pdfFileProvider.ReadAllLinesAsync(fileContent);
        return Log.Finish(result);
    }
}
