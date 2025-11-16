namespace AdminAssistant.Modules.DashboardModule.UI;

public interface IWidgetProvider
{
    IEnumerable<IDashboardWidget> GetWidgets();
}
