using System.ComponentModel;
using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
    }
    public abstract class ViewModelBase : PropertyChangedNotificationBase, IViewModelBase
    {
        protected ILoggingProvider Log { get; }

        public ViewModelBase(ILoggingProvider log)
        {
            this.Log = log;
        }
    }
}
