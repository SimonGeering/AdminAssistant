using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.BudgetModule;

internal sealed class BudgetViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IBudgetViewModel
{
    public string HeaderText => "Budget";
    public string SubHeaderText => string.Empty;
}
