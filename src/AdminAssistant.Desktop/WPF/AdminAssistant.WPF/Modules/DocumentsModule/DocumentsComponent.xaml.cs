using System.Windows.Controls;
using AdminAssistant.UI.Modules.DocumentsModule;

namespace AdminAssistant.WPF.Modules.DocumentsModule
{
    public partial class DocumentsComponent : UserControl
    {
        public DocumentsComponent()
        {
            this.InitializeComponent();
        }

        public DocumentsComponent(IDocumentsViewModel documentsViewModel)
            : this()
        {
            this.DataContext = documentsViewModel;
        }
    }
}
