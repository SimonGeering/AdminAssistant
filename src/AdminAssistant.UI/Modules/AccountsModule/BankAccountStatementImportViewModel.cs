using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule;

internal sealed class BankAccountStatementImportViewModel : ViewModelBase, IBankAccountStatementImportViewModel
{
    public BankAccountStatementImportViewModel(ILoggingProvider log)
        : base(log) => PageTitle = $"{HeaderText} - {SubHeaderText}";

    public string PageTitle { get; }
    public string HeaderText { get; } = "Accounts";
    public string SubHeaderText { get; } = "Bank Account Statement Import";
    public string FileName { get; private set; } = string.Empty;
    public long FileSize { get; private set; } = 0;

    // Embed PDF viewer
    // https://github.com/MudBlazor/MudBlazor/issues/1269
    // https://pdfobject.com/static/
    public string EmbeddedFileContentSrc { get; private set; } = string.Empty;

    public void ImportStatement (string contentType, string fileName, long fileSize, byte[] fileContent)
    {
        FileName = fileName;
        FileSize = fileSize;
        EmbeddedFileContentSrc = $"data:{contentType};base64,{Convert.ToBase64String(fileContent)}";

        // PDF File reader
        // https://www.nuget.org/packages/PDFsharp/6.0.0-preview-2
        // https://github.com/empira/PDFsharp
    }
}
