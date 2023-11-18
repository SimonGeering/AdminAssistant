using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.BillingModule;

internal sealed class BillingViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IBillingViewModel
{
    public string HeaderText => "Billing";
    public string SubHeaderText => string.Empty;
}
