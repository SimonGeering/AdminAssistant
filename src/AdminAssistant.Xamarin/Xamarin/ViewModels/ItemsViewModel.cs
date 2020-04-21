using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AdminAssistant.Models;
using AdminAssistant.Views;

namespace AdminAssistant.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            this.Title = "Browse";
            this.Items = new ObservableCollection<Item>();
            this.LoadItemsCommand = new Command(async () => await this.ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                this.Items.Add(newItem);
                await this.DataStore.AddItemAsync(newItem);
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            this.IsBusy = true;

            try
            {
                this.Items.Clear();
                var items = await this.DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    this.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
