using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Shared
{
    public interface IMainWindowViewModel : IViewModelBase, IRecipient<ModuleSelectionChangedMessage>
    {
        IViewModelBase SelectedViewModel { get; }
    }
}
