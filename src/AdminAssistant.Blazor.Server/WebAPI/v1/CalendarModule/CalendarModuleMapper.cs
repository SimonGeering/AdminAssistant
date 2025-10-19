using AdminAssistant.Modules.CalendarModule;

namespace AdminAssistant.WebAPI.v1.CalendarModule;

public static class CalendarModuleMapper
{
    public static IEnumerable<ReminderResponseDto> ToReminderResponseDtoEnumeration(this IEnumerable<Reminder> source)
        => source.Select(x => new ReminderResponseDto
        {
            ReminderID = x.ReminderID.Value,
            ReminderName = x.ReminderName
        });
}
