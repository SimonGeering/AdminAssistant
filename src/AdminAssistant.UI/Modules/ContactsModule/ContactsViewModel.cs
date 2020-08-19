using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.ContactsModule
{
    public class ContactsViewModel : ViewModelBase, IContactsViewModel
    {
        public ContactsViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Contacts";

        public string SubHeaderText => string.Empty;
    }
}
