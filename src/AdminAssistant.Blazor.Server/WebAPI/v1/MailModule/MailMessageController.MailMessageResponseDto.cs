namespace AdminAssistant.WebAPI.v1.MailModule;

public sealed record MailMessageResponseDto
{
    public int MailMessageID { get; init; }
    public string Subject { get; init; } = string.Empty;
}
