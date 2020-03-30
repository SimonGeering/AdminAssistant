using Xamarin.Forms;
using AdminAssistant.Services;

namespace AdminAssistant
{
    public partial class App : Application
    {

        public App()
        {
            this.InitializeComponent();

            DependencyService.Register<MockDataStore>();
            this.MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
