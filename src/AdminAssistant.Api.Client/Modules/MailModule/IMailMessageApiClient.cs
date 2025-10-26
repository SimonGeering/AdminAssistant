using AdminAssistant.WebAPI.v1.MailModule;

namespace AdminAssistant.WebAPIClient.v1.MailModule;

public interface IMailMessageApiClient
{
    [Get("/api/v1/mail-module/MailMessage")]
    Task<IEnumerable<MailMessageResponseDto>> GetMailMessagesAsync(CancellationToken cancellationToken = default);
}
