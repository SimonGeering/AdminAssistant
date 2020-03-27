using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AdminAssistant.Services;
using AdminAssistant.Views;

namespace AdminAssistant
{
    public partial class App : Application
    {

        public App()
        {
            this.InitializeComponent();

            DependencyService.Register<MockDataStore>();
            this.MainPage = new MainPage();
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
