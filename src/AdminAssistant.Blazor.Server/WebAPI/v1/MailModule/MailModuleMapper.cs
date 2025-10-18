using AdminAssistant.Modules.MailModule;

namespace AdminAssistant.WebAPI.v1.MailModule;

public static class MailModuleMapper
{
    public static IEnumerable<MailMessageResponseDto> ToMailMessageResponseDtoEnumeration(this IEnumerable<MailMessage> source)
        => source.Select(x =>  new MailMessageResponseDto
        {
            MailMessageID = x.MailMessageID.Value,
            Subject = x.Subject
        });
}
