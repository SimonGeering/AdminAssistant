using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.TasksModule.CQRS
{
    public record TaskListQuery : IRequest<Result<IEnumerable<TaskList>>>;
}
