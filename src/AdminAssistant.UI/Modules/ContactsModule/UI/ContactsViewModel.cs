namespace AdminAssistant.Modules.ContactsModule.UI;

public interface IContactsViewModel : IModuleViewModelBase;

internal sealed class ContactsViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IContactsViewModel
{
    public string HeaderText => "Contacts";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class ContactsDesignerViewModel
    : DesignerViewModelBase, IContactsViewModel
{
    public string HeaderText => "Contacts (Design Time)";
    public string SubHeaderText => string.Empty;
}
