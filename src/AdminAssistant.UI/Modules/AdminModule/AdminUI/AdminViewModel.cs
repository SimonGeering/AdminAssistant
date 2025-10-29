namespace AdminAssistant.Modules.AdminModule.AdminUI;

public interface IAdminViewModel : IModuleViewModelBase;

internal sealed class AdminViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IAdminViewModel
{
    public string HeaderText => "Admin";
    public string SubHeaderText => string.Empty;
}
public sealed class AdminDesignerViewModel
    : DesignerViewModelBase, IAdminViewModel
{
    public string HeaderText => "Admin (Design Time)";
    public string SubHeaderText => string.Empty;
}
