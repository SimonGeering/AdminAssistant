using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Shared.Breadcrumb
{
    public interface IBreadcrumbViewModel : IViewModelBase, IRecipient<ModeSelectionChangedMessage>, IRecipient<ModuleSelectionChangedMessage>
    {
        ModeSelectionItem ActiveMode { get; }

        ModuleSelectionItem ActiveModule { get; }
    }
}
