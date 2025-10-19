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
    IAdminAssistantWebAPIClient adminAssistantWebApiClient,
    ILoggingProvider log)
    : ServiceBase(adminAssistantWebApiClient, log), IAccountsService
{
    public async Task<List<BankAccountType>> LoadBankAccountTypesLookupDataAsync()
    {
        Log.Start();

        var response = await AdminAssistantWebAPIClient.GetBankAccountTypeAsync().ConfigureAwait(false);

        var result = new List<BankAccountType>(response.ToBankAccountTypeList());
        result.Insert(0, new BankAccountType() { BankAccountTypeID = BankAccountTypeId.Default, Description = string.Empty });

        return Log.Finish(result);
    }

    public async Task<BankAccount> CreateBankAccountAsync(BankAccount model)
    {
        Log.Start();

        var request = model.ToBankAccountCreateRequestDto();

        var response = await AdminAssistantWebAPIClient.PostBankAccountAsync(request).ConfigureAwait(false);

        var result = response.ToBankAccount();
        return Log.Finish(result);
    }

    public async Task<BankAccount> UpdateBankAccountAsync(BankAccount model)
    {
        Log.Start();

        var request = model.ToBankAccountUpdateRequestDto();

        var response = await AdminAssistantWebAPIClient.PutBankAccountAsync(request).ConfigureAwait(false);

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
