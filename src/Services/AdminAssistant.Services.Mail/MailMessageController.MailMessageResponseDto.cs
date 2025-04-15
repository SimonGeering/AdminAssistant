//using AdminAssistant.Modules.MailModule;

namespace AdminAssistant.WebAPI.v1.MailModule;

public sealed record MailMessageResponseDto //: IMapFrom<MailMessage>
{
    public int MailMessageID { get; init; }
    public string Subject { get; init; } = string.Empty;
}
