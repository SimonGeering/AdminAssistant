namespace AdminAssistant.DomainModel.Modules.CalendarModule.CQRS
{
    public sealed record ReminderQuery : IRequest<Result<IEnumerable<Reminder>>>;
}
