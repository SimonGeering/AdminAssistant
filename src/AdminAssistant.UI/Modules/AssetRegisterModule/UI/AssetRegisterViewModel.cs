namespace AdminAssistant.Modules.AssetRegisterModule.UI;

public interface IAssetRegisterViewModel : IModuleViewModelBase;

internal sealed class AssetRegisterViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IAssetRegisterViewModel
{
    public string HeaderText => "Asset Register";
    public string SubHeaderText => string.Empty;
}
