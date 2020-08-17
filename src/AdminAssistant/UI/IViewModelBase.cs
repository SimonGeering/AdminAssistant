using System;
using System.ComponentModel;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;

namespace AdminAssistant.UI
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        bool IsBusy { get; }
        event EventHandler<bool> IsBusyChanged;

        IAsyncCommand Loaded { get; }
    }
}
