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
    IAdminAssistantWebAPIClient adminAssistantWebAPIClient,
    IMapper mapper,
    ILoggingProvider log)
    : ServiceBase(adminAssistantWebAPIClient, mapper, log), IAccountsService
{
    public async Task<List<BankAccountType>> LoadBankAccountTypesLookupDataAsync()
    {
        Log.Start();

        var response = await AdminAssistantWebAPIClient.GetBankAccountTypeAsync().ConfigureAwait(false);

        var result = new List<BankAccountType>(Mapper.Map<IEnumerable<BankAccountType>>(response));
        result.Insert(0, new BankAccountType() { BankAccountTypeID = BankAccountTypeId.Default, Description = string.Empty });

        return Log.Finish(result);
    }

    public async Task<BankAccount> CreateBankAccountAsync(BankAccount model)
    {
        Log.Start();

        var request = Mapper.Map<BankAccountCreateRequestDto>(model);

        var response = await AdminAssistantWebAPIClient.PostBankAccountAsync(request).ConfigureAwait(false);

        var result = Mapper.Map<BankAccount>(response);
        return Log.Finish(result);
    }

    public async Task<BankAccount> UpdateBankAccountAsync(BankAccount model)
    {
        Log.Start();

        var request = Mapper.Map<BankAccountUpdateRequestDto>(model);

        var response = await AdminAssistantWebAPIClient.PutBankAccountAsync(request).ConfigureAwait(false);

        var result = Mapper.Map<BankAccount>(response);
        return Log.Finish(result);
    }

    public async Task<IEnumerable<string>> ParseBankAccountStatementAsync(byte[] fileContent)
    {
        Log.Start();

        var result = await pdfFileProvider.ReadAllLinesAsync(fileContent);
        return Log.Finish(result);
    }
}
