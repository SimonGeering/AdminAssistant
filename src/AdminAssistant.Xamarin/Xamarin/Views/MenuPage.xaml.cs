using AdminAssistant.Models;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace AdminAssistant.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        private readonly List<HomeMenuItem> menuItems;

        public MenuPage()
        {
            this.InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                await this.RootPage.NavigateFromMenu((int)((HomeMenuItem)e.SelectedItem).Id);
            };
        }
    }
}
