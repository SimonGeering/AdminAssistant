using System;
using System.Threading.Tasks;
using AdminAssistant.Infra.Providers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace AdminAssistant.UI
{
    public abstract class ViewModelBase : ObservableObject, IViewModelBase
    {
        [Obsolete("Replaced with OnLoadedAsync")]
        public virtual async Task OnInitializedAsync() => await OnLoadedAsync().ConfigureAwait(true);

        protected ILoggingProvider Log { get; }

        private bool _isBusy;
        public virtual bool IsBusy
        {
            get => _isBusy;
            protected set
            {
                SetProperty(ref _isBusy, value);
                OnIsBusyChanged(_isBusy);
            }
        }

        public event EventHandler<bool> IsBusyChanged = null!;

        protected void OnIsBusyChanged(bool isBusy) => IsBusyChanged?.Invoke(this, isBusy);
        

        public IAsyncRelayCommand Loaded { get; }

        public virtual async Task OnLoadedAsync() => await Task.CompletedTask.ConfigureAwait(false);

        public ViewModelBase(ILoggingProvider log)
        {
            Log = log;
            Loaded = new AsyncRelayCommand(execute: OnLoadedAsync);
        }
    }
}
