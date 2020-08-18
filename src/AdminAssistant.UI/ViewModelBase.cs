using System;
using System.Threading.Tasks;
using AdminAssistant.Framework.Providers;
using AsyncAwaitBestPractices.MVVM;
using Microsoft.Toolkit.Mvvm.ComponentModel;

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
        

        public IAsyncCommand Loaded { get; }

        public virtual async Task OnLoadedAsync() => await Task.CompletedTask.ConfigureAwait(false);

        public ViewModelBase(ILoggingProvider log)
        {
            this.Log = log;
            this.Loaded = new AsyncCommand(execute: this.OnLoadedAsync);
        }
    }
#if DEBUG
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA0414", Justification = "Designer Support")]
    public abstract class DesignTimeViewModelBase : ObservableObject, IViewModelBase
    {
        public Task OnInitializedAsync() => throw new System.NotImplementedException();

        public bool IsBusy { get; }

        public event EventHandler<bool> IsBusyChanged = null!;
            
        public IAsyncCommand Loaded { get; } = null!;
    }
#endif // DEBUG
}
