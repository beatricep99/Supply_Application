using Supply_Application.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace Supply_Application
{
    public partial class App : Application
    {
        static SupplyListDatabase database;


        public static SupplyListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SupplyListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ListEntryPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
