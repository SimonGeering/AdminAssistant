namespace AdminAssistant.Modules.BillingModule.UI;

public interface IBillingViewModel : IModuleViewModelBase;

internal sealed class BillingViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IBillingViewModel
{
    public string HeaderText => "Billing";
    public string SubHeaderText => string.Empty;
}
