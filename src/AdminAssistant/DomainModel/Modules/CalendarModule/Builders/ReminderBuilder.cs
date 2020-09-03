namespace AdminAssistant.DomainModel.Modules.CalendarModule.Builders
{
    public interface IReminderBuilder
    {
        Reminder Build();
        IReminderBuilder WithTestData(int reminderID = Constants.UnknownRecordID);
        IReminderBuilder WithReminderName(string reminderName);
    }
    internal class ReminderBuilder : Reminder, IReminderBuilder
    {
        public static Reminder Default(IReminderBuilder builder) => builder.Build();
        public static Reminder Default(ReminderBuilder builder) => builder.Build();

        public Reminder Build() => this;

        public IReminderBuilder WithTestData(int reminderID = Constants.UnknownRecordID)
        {
            ReminderID = reminderID;
            ReminderName = "Do something important";
            return this;
        }
        public IReminderBuilder WithReminderName(string reminderName)
        {
            ReminderName = reminderName;
            return this;
        }
    }
}
