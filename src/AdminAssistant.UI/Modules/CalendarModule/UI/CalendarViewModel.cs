namespace AdminAssistant.Modules.CalendarModule.UI;

public interface ICalendarViewModel : IModuleViewModelBase;

internal sealed class CalendarViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), ICalendarViewModel
{
    public string HeaderText => "Calendar";
    public string SubHeaderText => string.Empty;
}
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class CalendarDesignerViewModel
    : DesignerViewModelBase, ICalendarViewModel
{
    public string HeaderText => "Calendar (Designer)";
    public string SubHeaderText => string.Empty;
}
