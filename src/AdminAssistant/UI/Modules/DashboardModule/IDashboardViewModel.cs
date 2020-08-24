namespace AdminAssistant.UI.Modules.DashboardModule
{
    public interface IDashboardViewModel : IViewModelBase
    {
        string HeaderText { get; }
        string SubHeaderText { get; }
    }
}
