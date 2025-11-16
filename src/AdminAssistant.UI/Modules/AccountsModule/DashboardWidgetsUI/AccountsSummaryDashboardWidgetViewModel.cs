using AdminAssistant.Modules.DashboardModule.UI;

namespace AdminAssistant.Modules.AccountsModule.DashboardWidgetsUI;

public class AccountsSummaryDashboardWidgetViewModel : DashboardWidgetViewModel
{
    public const string AccountsSummaryDashboardWidgetKey = "AccountsSummaryDashboardWidget";

    public AccountsSummaryDashboardWidgetViewModel()
    {
        Key = AccountsSummaryDashboardWidgetKey;
        Title = "Accounts Summary";
        IsEnabled = true;
        Parameters = new Dictionary<string, object>();
    }
}
