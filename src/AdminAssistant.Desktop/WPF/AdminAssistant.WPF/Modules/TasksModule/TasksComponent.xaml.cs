using System.Windows.Controls;
using AdminAssistant.UI.Modules.TasksModule;

namespace AdminAssistant.WPF.Modules.TasksModule
{
    public partial class TasksComponent : UserControl
    {
        public TasksComponent()
        {
            this.InitializeComponent();
        }

        public TasksComponent(ITasksViewModel tasksViewModel)
            : this()
        {
            this.DataContext = tasksViewModel;
        }
    }
}
