using System.Threading.Tasks;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI
{
    public abstract class ViewModelBase : PropertyChangedNotificationBase, IViewModelBase
    {
        protected ILoggingProvider Log { get; }

        public ILoadingSpinner LoadingSpinner { get; }

        public virtual async Task OnInitializedAsync() => await Task.CompletedTask.ConfigureAwait(false);

        public ViewModelBase(ILoggingProvider log, ILoadingSpinner loadingSpinner)
        {
            this.Log = log;
            this.LoadingSpinner = loadingSpinner;
        }
    }

    public abstract class DesignTimeViewModelBase : PropertyChangedNotificationBase, IViewModelBase
    {
        public ILoadingSpinner LoadingSpinner => new DesignTimeLoadingSpinner();
        public Task OnInitializedAsync() => throw new System.NotImplementedException();
    }
}
