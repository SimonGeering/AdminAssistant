using AdminAssistant.Blazor.Client.Shared;
using AdminAssistant.UI.Modules;

namespace AdminAssistant.Blazor.Client.Modules;

public abstract class AdminAssistantModuleComponentBase<TViewModel> : AdminAssistantComponentBase<TViewModel>
    where TViewModel : IModuleViewModelBase
{
}
