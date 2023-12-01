namespace AdminAssistant.Modules.DocumentsModule;

public sealed record Document : IPersistable
{
    public const int FileNameMaxLength = 255;

    public DocumentId DocumentID { get; set; } = DocumentId.Default;

    public string FileName { get; set; } = string.Empty;

    public Id PrimaryKey => DocumentID;
}
public sealed record DocumentId(int Value) : Id(Value)
{
    public static DocumentId Default => new(Constants.UnknownRecordID);
}
