using System.Windows.Controls;
using AdminAssistant.UI.Modules.ContactsModule;

namespace AdminAssistant.WPF.Modules.ContactsModule
{
    public partial class ContactsComponent : UserControl
    {
        public ContactsComponent()
        {
            this.InitializeComponent();
        }
        public ContactsComponent(IContactsViewModel contactsViewModel)
            : this()
        {
            this.DataContext = contactsViewModel;
        }
    }
}
