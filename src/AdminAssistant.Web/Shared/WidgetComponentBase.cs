namespace AdminAssistant.Blazor.Client.Shared;

public abstract class WidgetComponentBase<TRuntimeViewModel, TDesignerViewModel>
    : AdminAssistantComponentBase<TRuntimeViewModel, TDesignerViewModel>
    where TRuntimeViewModel : class, IViewModelBase
    where TDesignerViewModel : class, TRuntimeViewModel
{
}
