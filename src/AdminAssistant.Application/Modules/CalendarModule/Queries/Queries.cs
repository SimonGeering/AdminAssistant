namespace AdminAssistant.Modules.CalendarModule.Queries;

public sealed record ReminderQuery : IRequest<Result<IEnumerable<Reminder>>>;
