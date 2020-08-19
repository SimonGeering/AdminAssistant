using System.Windows.Controls;
using AdminAssistant.UI.Shared.Header;

namespace AdminAssistant.WPF.Shared.Header
{
    public partial class HeaderComponent : UserControl
    {
        public HeaderComponent()
        {
            InitializeComponent();
        }
        public HeaderComponent(IHeaderViewModel headerViewModel)
            : this()
        {
            this.DataContext = headerViewModel;
        }
    }
}
