namespace AdminAssistant.Modules.BudgetModule.UI;

public interface IBudgetViewModel : IModuleViewModelBase;

internal sealed class BudgetViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IBudgetViewModel
{
    public string HeaderText => "Budget";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class BudgetDesignerViewModel
    : DesignerViewModelBase, IBudgetViewModel
{
    public string HeaderText => "Budget (Design Time)";
    public string SubHeaderText => string.Empty;
}
