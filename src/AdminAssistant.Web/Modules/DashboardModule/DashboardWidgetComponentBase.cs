using AdminAssistant.Blazor.Client.Shared;

namespace AdminAssistant.Blazor.Client.Modules.DashboardModule;

public abstract class DashboardWidgetComponentBase<TRuntimeViewModel, TDesignerViewModel>
    : WidgetComponentBase<TRuntimeViewModel, TDesignerViewModel>
    where TRuntimeViewModel : class, IViewModelBase
    where TDesignerViewModel : class, TRuntimeViewModel
{
}
