using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.DocumentsModule;

public record Document : IDatabasePersistable
{
    public const int FileNameMaxLength = 255;

    public int DocumentID { get; set; }

    public string FileName { get; set; } = string.Empty;

    public int PrimaryKey => DocumentID;
}
