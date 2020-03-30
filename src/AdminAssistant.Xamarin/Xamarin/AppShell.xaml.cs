using Xamarin.Forms;

namespace AdminAssistant
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            this.InitializeComponent();

            Routing.RegisterRoute("food/scan-barcode", typeof(Views.BarcodeScanPage));
            Routing.RegisterRoute("food/item-detail-edit", typeof(Views.ItemDetailPage));
            Routing.RegisterRoute("food/item-detail-add", typeof(Views.NewItemPage));

            Routing.RegisterRoute("about", typeof(Views.AboutPage));
        }

        private async void About_Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("about");
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
