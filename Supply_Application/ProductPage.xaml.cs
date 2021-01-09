using Supply_Application.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Supply_Application
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        SupplyList sl;
        ListProduct _listProduct;


        public ProductPage(SupplyList slist)
        {
            InitializeComponent();
            sl = slist; //pt a sti id-ul listei din SupplyList
            _listProduct = new ListProduct();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.SaveProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();

            var lp = new ListProduct()
            {
                SupplyListID = sl.ID,
                ProductID = product.ID
            };

            var listProducts = await App.Database.GetListProductAsyncTest(sl.ID);
            foreach (var listProduct in listProducts)
            {
                if (listProduct.ProductID == product.ID)
                    return;
            }

            _listProduct = lp;
            product.ListProducts = new List<ListProduct> { lp };

            await App.Database.SaveListProductAsync(_listProduct);
            ((Product)BindingContext).ID = 0;

            await Navigation.PopAsync();
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.DeleteProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }




        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Product product;
            if (e.SelectedItem != null)
            {
                product = e.SelectedItem as Product;

                BindingContext = product;
                //var lp = new ListProduct()
                //{
                //    SupplyListID = sl.ID,
                //    ProductID = p.ID
                //};

                //var listProducts = await App.Database.GetListProductAsyncTest(sl.ID);
                //foreach (var listProduct in listProducts){
                //    if (listProduct.ProductID == p.ID)
                //        return;
                //}

                //_listProduct = lp;
                //p.ListProducts = new List<ListProduct> { lp };
            }
        }
    }
}