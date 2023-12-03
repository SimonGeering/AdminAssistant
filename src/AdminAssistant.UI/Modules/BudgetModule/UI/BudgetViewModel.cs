namespace AdminAssistant.Modules.BudgetModule.UI;

public interface IBudgetViewModel : IModuleViewModelBase;

internal sealed class BudgetViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IBudgetViewModel
{
    public string HeaderText => "Budget";
    public string SubHeaderText => string.Empty;
}
