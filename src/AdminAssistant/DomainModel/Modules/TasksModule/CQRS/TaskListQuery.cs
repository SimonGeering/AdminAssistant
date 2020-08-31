using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.TasksModule.CQRS
{
    public class TaskListQuery : IRequest<Result<IEnumerable<TaskList>>>
    {
    }
}
