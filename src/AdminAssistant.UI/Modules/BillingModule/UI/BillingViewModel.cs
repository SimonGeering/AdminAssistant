namespace AdminAssistant.Modules.BillingModule.UI;

public interface IBillingViewModel : IModuleViewModelBase;

internal sealed class BillingViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IBillingViewModel
{
    public string HeaderText => "Billing";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class BillingDesignerViewModel
    : DesignerViewModelBase, IBillingViewModel
{
    public string HeaderText => "Billing (Design Time)";
    public string SubHeaderText => string.Empty;
}
