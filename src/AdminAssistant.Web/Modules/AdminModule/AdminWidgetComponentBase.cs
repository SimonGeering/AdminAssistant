using AdminAssistant.Blazor.Client.Shared;

namespace AdminAssistant.Blazor.Client.Modules.AdminModule;

public abstract class AdminWidgetComponentBase<TRuntimeViewModel, TDesignerViewModel>
    : WidgetComponentBase<TRuntimeViewModel, TDesignerViewModel>
    where TRuntimeViewModel : class, IViewModelBase
    where TDesignerViewModel : class, TRuntimeViewModel
{
}
