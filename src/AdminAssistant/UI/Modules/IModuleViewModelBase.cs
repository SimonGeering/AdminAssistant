namespace AdminAssistant.UI.Modules;

public interface IModuleViewModelBase : IViewModelBase
{
    string PageTitle => HeaderText;
    string HeaderText { get; }
    string SubHeaderText { get; }
}
