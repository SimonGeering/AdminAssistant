using System.Windows.Controls;
using AdminAssistant.UI.Modules.AssetRegisterModule;

namespace AdminAssistant.WPF.Modules.AssetRegisterModule
{
    public partial class AssetRegisterComponent : UserControl
    {
        public AssetRegisterComponent()
        {
            this.InitializeComponent();
        }

        public AssetRegisterComponent(IAssetRegisterViewModel assetRegisterViewModel)
            : this()
        {
            this.DataContext = assetRegisterViewModel;
        }
    }
}
