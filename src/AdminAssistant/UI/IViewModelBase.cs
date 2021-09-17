using System.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace AdminAssistant.UI
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        bool IsBusy { get; }
        event EventHandler<bool> IsBusyChanged;

        IAsyncRelayCommand Loaded { get; }
    }
}
