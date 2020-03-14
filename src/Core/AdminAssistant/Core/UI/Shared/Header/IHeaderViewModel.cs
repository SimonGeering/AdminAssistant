namespace AdminAssistant.Core.UI.Shared.Header
{
    public interface IHeaderViewModel : IViewModelBase
    {
        ModeSelectionItem ActiveMode { get; }
        ModuleSelectionItem ActiveModule { get; }
    }
}
