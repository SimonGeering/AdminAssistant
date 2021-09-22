using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.ContactsModule;

internal class ContactsViewModel : ViewModelBase, IContactsViewModel
{
    public ContactsViewModel(ILoggingProvider loggingProvider)
        : base(loggingProvider)
    {
    }

    public string HeaderText => "Contacts";

    public string SubHeaderText => string.Empty;
}
