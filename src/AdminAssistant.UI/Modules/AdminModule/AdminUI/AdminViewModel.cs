namespace AdminAssistant.Modules.AdminModule.AdminUI;

public interface IAdminViewModel : IModuleViewModelBase;

internal sealed class AdminViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IAdminViewModel
{
    public string HeaderText => "Admin";
    public string SubHeaderText => string.Empty;
}
