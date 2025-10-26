using AdminAssistant.Blazor.Client.Shared;

namespace AdminAssistant.Blazor.Client.Modules;

public abstract class AdminAssistantModuleComponentBase<TViewModel> : AdminAssistantComponentBase<TViewModel>
    where TViewModel : IModuleViewModelBase
{
}
