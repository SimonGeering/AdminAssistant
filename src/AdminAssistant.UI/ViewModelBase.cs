using System;
using System.Threading.Tasks;
using AdminAssistant.Framework.Providers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace AdminAssistant.UI
{
    public abstract class ViewModelBase : ObservableObject, IViewModelBase
    {
        [Obsolete("Replaced with OnLoadedAsync")]
        public virtual async Task OnInitializedAsync() => await OnLoadedAsync().ConfigureAwait(true);

        protected ILoggingProvider Log { get; }

        private bool isBusy;
        public virtual bool IsBusy
        {
            get => isBusy;
            protected set
            {
                this.SetProperty(ref isBusy, value);
                this.OnIsBusyChanged(isBusy);
            }
        }

        public event EventHandler<bool> IsBusyChanged = null!;

        protected void OnIsBusyChanged(bool isBusy) => this.IsBusyChanged?.Invoke(this, isBusy);
        

        public IAsyncRelayCommand Loaded { get; }

        public virtual async Task OnLoadedAsync() => await Task.CompletedTask.ConfigureAwait(false);

        public ViewModelBase(ILoggingProvider log)
        {
            this.Log = log;
            this.Loaded = new AsyncRelayCommand(execute: this.OnLoadedAsync);
        }
    }
#if DEBUG
    public abstract class DesignTimeViewModelBase : ObservableObject, IViewModelBase
    {
        public Task OnInitializedAsync() => throw new System.NotImplementedException();

        public bool IsBusy { get; }
#pragma warning disable CS0414 // Assigned but never used

        public event EventHandler<bool> IsBusyChanged = null!;
#pragma warning restore CS0414
        public IAsyncRelayCommand Loaded { get; } = null!;
    }
#endif // DEBUG
}
