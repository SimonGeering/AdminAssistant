namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IBankAccountStatementImportViewModel : IModuleViewModelBase
{
    string FileName { get; }
    long FileSize { get; }
    string EmbeddedFileContentSrc { get; }

    Task ImportStatementAsync (string contentType, string fileName, long fileSize, byte[] fileContent);
}
internal sealed class BankAccountStatementImportViewModel : ViewModelBase, IBankAccountStatementImportViewModel
{
    private readonly IAccountsService _accountsService;

    public BankAccountStatementImportViewModel(IAccountsService accountsService, ILoggingProvider log)
        : base(log)
    {
        _accountsService = accountsService;

        PageTitle = $"{HeaderText} - {SubHeaderText}";
    }

    public string PageTitle { get; }
    public string HeaderText { get; } = "Accounts";
    public string SubHeaderText { get; } = "Bank Account Statement Import";
    public string FileName { get; private set; } = string.Empty;
    public long FileSize { get; private set; } = 0;

    // Embed PDF viewer
    // https://github.com/MudBlazor/MudBlazor/issues/1269
    // https://pdfobject.com/static/
    public string EmbeddedFileContentSrc { get; private set; } = string.Empty;

    public async Task ImportStatementAsync (string contentType, string fileName, long fileSize, byte[] fileContent)
    {
        FileName = fileName;
        FileSize = fileSize;
        EmbeddedFileContentSrc = $"data:{contentType};base64,{Convert.ToBase64String(fileContent)}";

        await _accountsService.ParseBankAccountStatementAsync(fileContent);

        // TODO: Display the result in a grid.
    }
}
