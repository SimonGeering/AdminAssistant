namespace AdminAssistant.Modules.AssetRegisterModule.UI;

public interface IAssetRegisterViewModel : IModuleViewModelBase;

internal sealed class AssetRegisterViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IAssetRegisterViewModel
{
    public string HeaderText => "Asset Register";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class AssetRegisterDesignerViewModel
    : DesignerViewModelBase, IAssetRegisterViewModel
{
    public string HeaderText => "Asset Register (Demo Data)";
    public string SubHeaderText => string.Empty;
}
