using AdminAssistant.Shared.UI;

namespace AdminAssistant.UI;

#if DEBUG
public static class DesignTime
{
    public static IMainWindowViewModel MainWindowViewModel => new MainWindowViewModel();
}
#endif // DEBUG
