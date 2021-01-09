using Supply_Application.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Supply_Application
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        ListProduct _listProduct;
        public ListPage()
        {
            _listProduct = new ListProduct();
            BindingContext = new SupplyList();
            InitializeComponent();
        }

        public ListPage(SupplyList supplyList)
        {
            _listProduct = new ListProduct();
            BindingContext = supplyList;
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (SupplyList)BindingContext;
            slist.Date = DateTime.UtcNow;
            await App.Database.SaveSupplyListAsync(slist);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            await App.Database.DeleteListProductAsync(_listProduct);

            await Navigation.PopAsync();
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((SupplyList) BindingContext)
            {
                BindingContext = new Product()
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (SupplyList)BindingContext;
              
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
         }


        //dupa selectarea unui produs sa se adauge in lista de cump. si se intoarce in List Page
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product p;
            if (e.SelectedItem != null)
            {
                var supplyList = (SupplyList)BindingContext;

                p = e.SelectedItem as Product;

                var listProducts = await App.Database.GetListProductAsyncTest(supplyList.ID);
                foreach (var listProduct in listProducts)
                {
                    if (listProduct.SupplyListID == supplyList.ID && listProduct.ProductID == p.ID)
                         _listProduct = listProduct;
                }

            }
        }
    }
}