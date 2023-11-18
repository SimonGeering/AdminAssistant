using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AssetRegisterModule;

internal sealed class AssetRegisterViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IAssetRegisterViewModel
{
    public string HeaderText => "Asset Register";
    public string SubHeaderText => string.Empty;
}
