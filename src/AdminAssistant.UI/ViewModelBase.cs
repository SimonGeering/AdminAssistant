using System;
using System.Threading.Tasks;
using AdminAssistant.Framework.Providers;
using AsyncAwaitBestPractices.MVVM;

namespace AdminAssistant.UI
{
    public abstract class ViewModelBase : PropertyChangedNotificationBase, IViewModelBase
    {
        [Obsolete("Replaced with OnLoadedAsync")]
        public virtual async Task OnInitializedAsync() => await OnLoadedAsync().ConfigureAwait(true);

        protected ILoggingProvider Log { get; }

        private bool isBusy;

        public virtual bool IsBusy
        {
            get => isBusy; protected set
            {
                if (isBusy == value)
                    return;

                isBusy = value;
                this.OnPropertyChanged();
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
    public abstract class DesignTimeViewModelBase : PropertyChangedNotificationBase, IViewModelBase
    {
        public Task OnInitializedAsync() => throw new System.NotImplementedException();

        public bool IsBusy { get; }
        public event EventHandler<bool> IsBusyChanged = null!;
            
        public IAsyncCommand Loaded { get; } = null!;
    }
#endif // DEBUG
}
