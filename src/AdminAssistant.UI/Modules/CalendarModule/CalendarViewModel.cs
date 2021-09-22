using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.CalendarModule;

internal class CalendarViewModel : ViewModelBase, ICalendarViewModel
{
    public CalendarViewModel(ILoggingProvider loggingProvider)
        : base(loggingProvider)
    {
    }

    public string HeaderText => "Calendar";

    public string SubHeaderText => string.Empty;
}
