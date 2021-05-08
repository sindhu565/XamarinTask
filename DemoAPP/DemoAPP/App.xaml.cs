using DemoAPP.DBFolder;
using DemoAPP.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoAPP
{
    public partial class App : Application
    {
        static UserDataBase database;

        public static UserDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new UserDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RegisterPage())
            {
                BarBackgroundColor = Color.FromHex("#ff5300"),
                BarTextColor = Color.White,
            };
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
