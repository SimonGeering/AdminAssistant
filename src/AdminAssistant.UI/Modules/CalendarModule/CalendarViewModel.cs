using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.CalendarModule;

internal sealed class CalendarViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), ICalendarViewModel
{
    public string HeaderText => "Calendar";
    public string SubHeaderText => string.Empty;
}
