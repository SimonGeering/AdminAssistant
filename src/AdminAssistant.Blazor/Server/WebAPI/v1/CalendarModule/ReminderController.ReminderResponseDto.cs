using AdminAssistant.DomainModel.Modules.CalendarModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.CalendarModule
{
    public class ReminderResponseDto : IMapFrom<Reminder>
    {
        public int ReminderID { get; set; }
        public string ReminderName { get; set; } = string.Empty;
    }
}
