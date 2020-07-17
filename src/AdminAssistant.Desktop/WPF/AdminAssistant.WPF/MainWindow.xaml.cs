using System.Windows;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;

namespace AdminAssistant.WPF
{
    public partial class MainWindow : ChromelessWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();

            SfSkinManager.ApplyStylesOnApplication = true;
        }
    }
}
