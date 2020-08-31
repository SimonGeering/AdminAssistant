using AdminAssistant.DomainModel.Modules.TasksModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.TasksModule
{
    public class TaskListResponseDto : IMapFrom<TaskList>
    {
        public int TaskListID { get; set; }
        public string TaskListName { get; set; } = string.Empty;
    }
}
