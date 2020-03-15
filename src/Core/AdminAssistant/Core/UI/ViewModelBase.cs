using System.ComponentModel;

namespace AdminAssistant.Core.UI
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
    }
    public abstract class ViewModelBase : PropertyChangedNotificationBase, IViewModelBase
    {
    }
}
