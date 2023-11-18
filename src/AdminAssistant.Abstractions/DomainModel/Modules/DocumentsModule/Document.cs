namespace AdminAssistant.DomainModel.Modules.DocumentsModule;

public sealed record Document : IDatabasePersistable
{
    public const int FileNameMaxLength = 255;

    public int DocumentID { get; set; }

    public string FileName { get; set; } = string.Empty;

    public int PrimaryKey => DocumentID;
}
