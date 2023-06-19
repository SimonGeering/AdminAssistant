namespace AdminAssistant.UI.Modules.AccountsModule;

public interface IBankAccountStatementImportViewModel : IModuleViewModelBase
{
    string FileName { get; }
    long FileSize { get; }
    string EmbeddedFileContentSrc { get; }

    void ImportStatement (string contentType, string fileName, long fileSize, byte[] fileContent);
}
