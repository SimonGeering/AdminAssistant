namespace AdminAssistant.DomainModel.Modules.CalendarModule.Builders
{
    public interface IReminderBuilder
    {
        Reminder Build();
        IReminderBuilder WithTestData(int reminderID = Constants.UnknownRecordID);
        IReminderBuilder WithReminderName(string reminderName);
    }
    internal class ReminderBuilder : IReminderBuilder
    {
        private Reminder _reminder = new();

        public static Reminder Default(IReminderBuilder builder) => builder.Build();
        public static Reminder Default(ReminderBuilder builder) => builder.Build();

        public Reminder Build() => _reminder;

        public IReminderBuilder WithTestData(int reminderID = Constants.UnknownRecordID)
        {
            _reminder = _reminder with
            {
                ReminderID = reminderID,
                ReminderName = "Do something important"
            };
            return this;
        }
        public IReminderBuilder WithReminderName(string reminderName)
        {
            _reminder = _reminder with { ReminderName = reminderName };
            return this;
        }
    }
}
