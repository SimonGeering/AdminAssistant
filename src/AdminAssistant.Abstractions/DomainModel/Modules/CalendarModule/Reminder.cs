namespace AdminAssistant.DomainModel.Modules.CalendarModule;

public sealed record Reminder : IDatabasePersistable
{
    public const int ReminderNameMaxLength = Constants.NameMaxLength;

    public ReminderId ReminderID { get; set; } = ReminderId.Default;
    public string ReminderName { get; set; } = string.Empty;

    public Id PrimaryKey => ReminderID;
}
public sealed record ReminderId(int Value) : Id(Value)
{
    public static ReminderId Default => new(Constants.UnknownRecordID);
}
