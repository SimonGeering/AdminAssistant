using AdminAssistant.Blazor.Client.Shared;

namespace AdminAssistant.Blazor.Client.Modules;

public abstract class AdminAssistantModuleComponentBase<TRuntimeViewModel, TDesignerViewModel>
    : AdminAssistantComponentBase<TRuntimeViewModel, TDesignerViewModel>
    where TRuntimeViewModel : class, IViewModelBase
    where TDesignerViewModel : class, TRuntimeViewModel
{
}
