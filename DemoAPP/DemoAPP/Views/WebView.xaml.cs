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
    public partial class WebView : ContentPage
    {
        public WebView(ArticlesItems newsView)
        {
            this.UrlList = newsView;
            InitializeComponent();
            webView.Source = UrlList.WebUrl;

        }
        private void webView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
               
                  var data = webView.EvaluateJavaScriptAsync("document.body.innerHTML");
                ActivityIndicatorView.IsRunning = false;
            }
            catch (Exception ex)
            {
               
            }
        }

        private void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            ActivityIndicatorView.IsRunning = true;
        }

        public ArticlesItems UrlList { get; set; }
        public string NewsUrl { get; set; }

    }
}