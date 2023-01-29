using AdminAssistant.Blazor.Shared;
using AdminAssistant.UI.Modules;

namespace AdminAssistant.Blazor.Modules;

public abstract class AdminAssistantModuleComponentBase<TViewModel> : AdminAssistantComponentBase<TViewModel>
    where TViewModel : IModuleViewModelBase
{
}
