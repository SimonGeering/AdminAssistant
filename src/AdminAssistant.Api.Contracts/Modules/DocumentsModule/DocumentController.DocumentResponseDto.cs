namespace AdminAssistant.WebAPI.v1.DocumentsModule;

public sealed record DocumentResponseDto
{
    public int DocumentID { get; init; }
    public string FileName { get; init; } = string.Empty;
}
