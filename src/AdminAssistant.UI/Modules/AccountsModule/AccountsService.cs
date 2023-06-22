using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using AutoMapper;
using MediatR;

namespace AdminAssistant.UI.Modules.AccountsModule;

internal sealed class AccountsService : ServiceBase, IAccountsService
{
    private readonly IPdfFileProvider _pdfFileProvider;
    private readonly IMediator _mediator;

    public AccountsService(IPdfFileProvider pdfFileProvider, IMediator mediator, IAdminAssistantWebAPIClient adminAssistantWebAPIClient, IMapper mapper, ILoggingProvider log)
        : base(adminAssistantWebAPIClient, mapper, log)
    {
        _pdfFileProvider = pdfFileProvider;
        _mediator = mediator;
    }

    public async Task<List<BankAccountType>> LoadBankAccountTypesLookupDataAsync()
    {
        Log.Start();

        var response = await AdminAssistantWebAPIClient.GetBankAccountTypeAsync().ConfigureAwait(false);

        var result = new List<BankAccountType>(Mapper.Map<IEnumerable<BankAccountType>>(response));
        result.Insert(0, new BankAccountType() { BankAccountTypeID = Constants.UnknownRecordID, Description = string.Empty });

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

        var result = await _pdfFileProvider.ReadAllLinesAsync(fileContent);
        return Log.Finish(result);
    }
}
