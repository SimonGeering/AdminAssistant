namespace AdminAssistant.DomainModel.Modules.CalendarModule.Builders;

public interface IReminderBuilder
{
    Reminder Build();
    IReminderBuilder WithTestData(int reminderID = Constants.UnknownRecordID);
    IReminderBuilder WithReminderName(string reminderName);
}
