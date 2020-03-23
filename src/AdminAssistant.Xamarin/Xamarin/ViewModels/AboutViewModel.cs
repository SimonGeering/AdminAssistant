using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AdminAssistant.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));

            AppName = "Admin Assistant";
            AppVersion = SupportFunctions.Version;
        }

        public ICommand OpenWebCommand { get; }

        public string AppName { get; }
        public string AppVersion { get; }
    }
}
