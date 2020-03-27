using System;
using System.ComponentModel;
using Xamarin.Forms;
using AdminAssistant.Models;

namespace AdminAssistant.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            this.InitializeComponent();

            this.Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            this.BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", this.Item);
            await this.Navigation.PopModalAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e) => await this.Navigation.PopModalAsync();
    }
}
