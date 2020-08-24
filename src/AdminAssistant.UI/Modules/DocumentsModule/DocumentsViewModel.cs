using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.DocumentsModule
{
    public class DocumentsViewModel : ViewModelBase, IDocumentsViewModel
    {
        public DocumentsViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Documents";

        public string SubHeaderText => string.Empty;
    }
}
