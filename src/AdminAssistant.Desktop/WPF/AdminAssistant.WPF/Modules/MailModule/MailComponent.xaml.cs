using System.Windows.Controls;
using AdminAssistant.UI.Modules.MailModule;

namespace AdminAssistant.WPF.Modules.MailModule
{
    public partial class MailComponent : UserControl
    {
        public MailComponent()
        {
            this.InitializeComponent();
        }

        public MailComponent(IMailViewModel mailViewModel)
            : this()
        {
            this.DataContext = mailViewModel;
        }
    }
}
