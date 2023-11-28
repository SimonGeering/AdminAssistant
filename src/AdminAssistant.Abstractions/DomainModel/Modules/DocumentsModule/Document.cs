namespace AdminAssistant.DomainModel.Modules.DocumentsModule;

public sealed record Document : IDatabasePersistable
{
    public const int FileNameMaxLength = 255;

    public DocumentId DocumentID { get; set; } = DocumentId.Default;

    public string FileName { get; set; } = string.Empty;

    public int PrimaryKey => DocumentID.Value;
}
