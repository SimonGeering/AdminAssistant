namespace AdminAssistant.Modules.ContactsModule.UI;

public interface IContactsViewModel : IModuleViewModelBase;

internal sealed class ContactsViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IContactsViewModel
{
    public string HeaderText => "Contacts";
    public string SubHeaderText => string.Empty;
}
