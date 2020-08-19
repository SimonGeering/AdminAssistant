using System.Windows.Controls;
using AdminAssistant.UI.Shared.Footer;

namespace AdminAssistant.WPF.Shared.Footer
{
    public partial class FooterComponent : UserControl
    {
        public FooterComponent()
        {
            InitializeComponent();
        }

        public FooterComponent(IFooterViewModel footerViewModel)
            : this()
        {
            this.DataContext = footerViewModel;
        }
    }
}
