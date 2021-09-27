namespace AdminAssistant.DomainModel.Modules.CalendarModule.CQRS
{
    public record ReminderQuery : IRequest<Result<IEnumerable<Reminder>>>;
}
