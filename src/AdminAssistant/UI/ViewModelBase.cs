using System.ComponentModel;

namespace AdminAssistant.UI
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
    }
    public abstract class ViewModelBase : PropertyChangedNotificationBase, IViewModelBase
    {
    }
}
