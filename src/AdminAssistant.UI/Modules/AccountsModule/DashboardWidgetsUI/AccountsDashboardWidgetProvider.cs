using AdminAssistant.Modules.DashboardModule.UI;

namespace AdminAssistant.Modules.AccountsModule.DashboardWidgetsUI;

public class AccountsDashboardWidgetProvider : IWidgetProvider
{
    public IEnumerable<IDashboardWidget> GetWidgets()
    {
        yield return new AccountsSummaryDashboardWidgetViewModel();
    }
}
