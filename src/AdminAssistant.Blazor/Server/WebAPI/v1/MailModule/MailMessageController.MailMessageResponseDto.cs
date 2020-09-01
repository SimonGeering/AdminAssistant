using AdminAssistant.DomainModel.Modules.MailModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.MailModule
{
    public class MailMessageResponseDto : IMapFrom<MailMessage>
    {
        public int MailMessageID { get; set; }
        public string Subject { get; set; } = string.Empty;
    }
}
