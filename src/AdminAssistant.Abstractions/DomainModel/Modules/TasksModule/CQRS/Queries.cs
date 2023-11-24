namespace AdminAssistant.DomainModel.Modules.TasksModule.CQRS
{
    public sealed record TaskListQuery : IRequest<Result<IEnumerable<TaskList>>>;
}
