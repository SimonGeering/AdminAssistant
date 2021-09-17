using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.BillingModule
{
    internal class BillingViewModel : ViewModelBase, IBillingViewModel
    {
        public BillingViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Billing";

        public string SubHeaderText => string.Empty;
    }
}
