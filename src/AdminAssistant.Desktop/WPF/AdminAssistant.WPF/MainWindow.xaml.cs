using AdminAssistant.UI.Shared;

namespace AdminAssistant.WPF
{
    public partial class MainWindow : Syncfusion.Windows.Shared.ChromelessWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }
        public MainWindow(IMainWindowViewModel viewModel)
            : this()
        {
            this.DataContext = viewModel;
        }
    }
}
