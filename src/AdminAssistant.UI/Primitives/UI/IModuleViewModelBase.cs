namespace AdminAssistant.Primitives.UI;

public interface IModuleViewModelBase : IViewModelBase
{
    string PageTitle => HeaderText;
    string HeaderText { get; }
    string SubHeaderText { get; }
}
