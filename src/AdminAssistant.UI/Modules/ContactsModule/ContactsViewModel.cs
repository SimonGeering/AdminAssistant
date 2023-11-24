using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.ContactsModule;

internal sealed class ContactsViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IContactsViewModel
{
    public string HeaderText => "Contacts";
    public string SubHeaderText => string.Empty;
}
