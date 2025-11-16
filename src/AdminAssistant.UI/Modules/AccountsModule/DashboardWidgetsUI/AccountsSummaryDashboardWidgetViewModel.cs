using AdminAssistant.Modules.DashboardModule.UI;

namespace AdminAssistant.Modules.AccountsModule.DashboardWidgetsUI;

public interface IAccountsSummaryDashboardWidgetViewModel : IViewModelBase
{
    public const string AccountsSummaryDashboardWidgetKey = "AccountsSummaryDashboardWidget";
}
internal sealed class AccountsSummaryDashboardWidgetViewModel : DashboardWidgetViewModelBase, IAccountsSummaryDashboardWidgetViewModel
{
    public AccountsSummaryDashboardWidgetViewModel(ILoggingProvider log) : base(log)
    {
        Key = IAccountsSummaryDashboardWidgetViewModel.AccountsSummaryDashboardWidgetKey;
        Title = "Accounts Summary";
        IsEnabled = true;
    }
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class AccountsSummaryDashboardWidgetDesignerViewModel
    : DesignerViewModelBase, IAccountsSummaryDashboardWidgetViewModel
{
    public IEnumerable<Bank> Banks => [
        new Bank() { BankID = new(1), BankName = new("Barclays Bank plc") },
        new Bank() { BankID = new(2), BankName = new("HSBC Bank (UK) Limited") },
        new Bank() { BankID = new(3), BankName = new("Santander UK Plc") }
    ];
}
