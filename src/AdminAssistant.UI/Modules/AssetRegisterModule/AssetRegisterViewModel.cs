using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AssetRegisterModule;

internal sealed class AssetRegisterViewModel : ViewModelBase, IAssetRegisterViewModel
{
    public AssetRegisterViewModel(ILoggingProvider loggingProvider)
        : base(loggingProvider)
    {
    }

    public string HeaderText => "Asset Register";

    public string SubHeaderText => string.Empty;
}
