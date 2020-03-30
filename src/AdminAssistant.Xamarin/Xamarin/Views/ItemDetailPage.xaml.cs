using System;
using System.ComponentModel;
using Xamarin.Forms;
using AdminAssistant.Models;
using AdminAssistant.ViewModels;

namespace AdminAssistant.Views
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class ItemDetailPage : ContentPage
	{
		private readonly ItemDetailViewModel viewModel;

		public ItemDetailPage(ItemDetailViewModel viewModel)
		{
			this.InitializeComponent();

			this.BindingContext = this.viewModel = viewModel;
		}

		public ItemDetailPage()
		{
			this.InitializeComponent();

			var item = new Item
			{
				Text = "Item 1",
				Description = "This is an item description."
			};

			viewModel = new ItemDetailViewModel(item);
			this.BindingContext = viewModel;
		}
	}
}
