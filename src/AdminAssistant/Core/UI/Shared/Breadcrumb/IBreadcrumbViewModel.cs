namespace AdminAssistant.Core.UI.Shared.Breadcrumb
{
    public interface IBreadcrumbViewModel : IViewModelBase
    {
        ModeSelectionItem ActiveMode { get; }

        ModuleSelectionItem ActiveModule { get; }
    }
}
