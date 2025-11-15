namespace AdminAssistant.Modules.ReportsModule.UI;

public interface IReportsViewModel : IModuleViewModelBase;

internal sealed class ReportsViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IReportsViewModel
{
    public string HeaderText => "Reports";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class ReportsDesignerViewModel : DesignerViewModelBase, IReportsViewModel
{
    public string HeaderText => "Reports (Design Time)";
    public string SubHeaderText => string.Empty;
}
