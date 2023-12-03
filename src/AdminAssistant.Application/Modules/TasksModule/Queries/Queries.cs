namespace AdminAssistant.Modules.TasksModule.Queries
{
    public sealed record TaskListQuery : IRequest<Result<IEnumerable<TaskList>>>;
}
