using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.TasksModule
{
    public class TaskList : IDatabasePersistable
    {
        public const int TaskListNameMaxLength = Constants.NameMaxLength;

        public int TaskListID { get; set; }
        public string TaskListName { get; set; } = string.Empty;

        public int PrimaryKey => TaskListID;
    }
}
