using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Shared.Header
{
    public interface IHeaderViewModel : IViewModelBase, IRecipient<ModeSelectionChangedMessage>, IRecipient<ModuleSelectionChangedMessage>
    {
        ModeSelectionItem ActiveMode { get; }
        ModuleSelectionItem ActiveModule { get; }
    }
}
