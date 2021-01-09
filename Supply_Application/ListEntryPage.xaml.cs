using Supply_Application.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Supply_Application
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEntryPage : ContentPage
    {
        public ListEntryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetSupplyListsAsync();
        }

        async void OnSupplyListAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListPage
            {
                BindingContext = new SupplyList()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ProductSupplyPage(e.SelectedItem as SupplyList));
            }
        }
    }
}