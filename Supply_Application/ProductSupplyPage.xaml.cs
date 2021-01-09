using Supply_Application.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Supply_Application
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductSupplyPage : ContentPage
    {
        public ProductSupplyPage()
        {
            InitializeComponent();
        }

        public ProductSupplyPage(SupplyList supply)
        {
            InitializeComponent();

            BindingContext = supply;
        }
        async void Edit_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ListPage(BindingContext as SupplyList));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (SupplyList)BindingContext;

            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }
    }
}