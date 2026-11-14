using AdminAssistant.Blazor.Client.Shared;

namespace AdminAssistant.Blazor.Client.Modules.AdminModule;

#pragma warning disable CA1515 // Class must be public as used for blazor pages / components
public abstract class AdminWidgetComponentBase<TRuntimeViewModel, TDesignerViewModel>
#pragma warning restore CA1515
    : WidgetComponentBase<TRuntimeViewModel, TDesignerViewModel>
    where TRuntimeViewModel : class, IViewModelBase
    where TDesignerViewModel : class, TRuntimeViewModel
{
}
