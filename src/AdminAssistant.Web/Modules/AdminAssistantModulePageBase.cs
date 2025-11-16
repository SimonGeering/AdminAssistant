using AdminAssistant.Blazor.Client.Shared;

namespace AdminAssistant.Blazor.Client.Modules;

public abstract class AdminAssistantModulePageBase<TRuntimeViewModel, TDesignerViewModel>
    : AdminAssistantComponentBase<TRuntimeViewModel, TDesignerViewModel>
    where TRuntimeViewModel : class, IModuleViewModelBase
    where TDesignerViewModel : class, TRuntimeViewModel
{
}
