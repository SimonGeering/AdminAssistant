namespace AdminAssistant.WebAPI.v1.CalendarModule;

public sealed record ReminderResponseDto
{
    public int ReminderID { get; init; }
    public string ReminderName { get; init; } = string.Empty;
}
