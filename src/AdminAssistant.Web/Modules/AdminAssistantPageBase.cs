using AdminAssistant.Blazor.Client.Shared;

namespace AdminAssistant.Blazor.Client.Modules;

public abstract class AdminAssistantPageComponentBase<TRuntimeViewModel, TDesignerViewModel>
    : AdminAssistantComponentBase<TRuntimeViewModel, TDesignerViewModel>
    where TRuntimeViewModel : class, IViewModelBase
    where TDesignerViewModel : class, TRuntimeViewModel
{
}
