using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AdminAssistant.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            this.Title = "About";
            this.OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));

            this.AppName = "Admin Assistant";
            this.AppVersion = SupportFunctions.Version;
        }

        public ICommand OpenWebCommand { get; }

        public string AppName { get; }
        public string AppVersion { get; }
    }
}
