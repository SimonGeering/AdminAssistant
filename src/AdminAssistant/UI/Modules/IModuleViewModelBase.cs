namespace AdminAssistant.UI.Modules;

public interface IModuleViewModelBase : IViewModelBase
{
    string HeaderText { get; }
    string SubHeaderText { get; }
}
