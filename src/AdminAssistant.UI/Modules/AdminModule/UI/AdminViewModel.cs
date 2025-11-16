namespace AdminAssistant.Modules.AdminModule.UI;

public interface IAdminViewModel : IModuleViewModelBase
{
    IReadOnlyCollection<AdminToolViewModel> Tools { get; }
}

internal sealed class AdminViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IAdminViewModel
{
    public string HeaderText => "Admin";
    public string SubHeaderText => string.Empty;

    public IReadOnlyCollection<AdminToolViewModel> Tools { get; } = Array.Empty<AdminToolViewModel>();
}
public sealed class AdminDesignerViewModel
    : DesignerViewModelBase, IAdminViewModel
{
    public string HeaderText => "Admin (Demo Data)";
    public string SubHeaderText => string.Empty;

    public IReadOnlyCollection<AdminToolViewModel> Tools { get; } = new List<AdminToolViewModel>
    {
        new AdminToolViewModel()
        {
            Key = "Demo.Accounts",
            Title = "Mock Accounts Summary",
            IsEnabled = true,
        },
        new AdminToolViewModel()
        {
            Key = "Demo.Tasks",
            Title = "Mock Task List",
            IsEnabled = true,
        }
    };
}
