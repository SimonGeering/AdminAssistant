using AdminAssistant.WebAPI.v1.CalendarModule;

namespace AdminAssistant.WebAPIClient.v1.CalendarModule;

public interface IReminderApiClient
{
    [Get("/api/v1/calendar-module/Reminder")]
    Task<IEnumerable<ReminderResponseDto>> GetRemindersAsync(CancellationToken cancellationToken = default);
}
