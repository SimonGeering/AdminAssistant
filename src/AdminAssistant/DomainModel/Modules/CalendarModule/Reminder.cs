using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.CalendarModule;

public record Reminder : IDatabasePersistable
{
    public const int ReminderNameMaxLength = Constants.NameMaxLength;

    public int ReminderID { get; set; }
    public string ReminderName { get; set; } = string.Empty;

    public int PrimaryKey => ReminderID;
}
