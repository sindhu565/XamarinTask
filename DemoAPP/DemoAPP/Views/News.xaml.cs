using DemoAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DemoAPP.ViewModels.NewsViewModel;

namespace DemoAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class News : ContentPage
    {
        NewsViewModel _viewModel = NewsViewModel.GetInstance();

        public News()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }
        protected override void OnAppearing()
        {
            listView.ItemsSource = _viewModel.NewsList;
           
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var newsURL = (sender as StackLayout).BindingContext as ArticlesItems;
            Navigation.PushAsync(new WebView(newsURL));
           // await((AppShell)App.Current.MainPage).CurrentItem.CurrentItem.Navigation.PushAsync(new WebView(newsURL));
        }
    }
}