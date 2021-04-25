using AdminAssistant.DomainModel.Modules.CalendarModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.CalendarModule
{
    public record ReminderResponseDto : IMapFrom<Reminder>
    {
        public int ReminderID { get; init; }
        public string ReminderName { get; init; } = string.Empty;
    }
}
