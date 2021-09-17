using AdminAssistant.DomainModel.Modules.MailModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.MailModule
{
    public record MailMessageResponseDto : IMapFrom<MailMessage>
    {
        public int MailMessageID { get; init; }
        public string Subject { get; init; } = string.Empty;
    }
}
