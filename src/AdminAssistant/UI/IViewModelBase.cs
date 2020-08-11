using System.ComponentModel;
using System.Threading.Tasks;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        ILoadingSpinner LoadingSpinner { get; }
    }
}
