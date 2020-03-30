using System;
using System.ComponentModel;
using AdminAssistant.Models;
using AdminAssistant.ViewModels;
using Xamarin.Forms;

namespace AdminAssistant.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        private readonly ItemsViewModel viewModel;

        public ItemsPage()
        {
            this.InitializeComponent();

            this.BindingContext = viewModel = new ItemsViewModel();
        }

        public async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await this.Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        public async void AddItem_Clicked(object sender, EventArgs e)
        {
            //await this.Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            await Shell.Current.GoToAsync("food/item-detail-add");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}
