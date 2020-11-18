using DemoAPP.Views;
using MvvmCross.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPP.ViewModels
{
    public class NewsViewModel : BaseviewModel

    {
        public IMvxNavigationService navigationService;
        private static NewsViewModel _instance = new NewsViewModel();
        public static NewsViewModel GetInstance()
        {
            return _instance;

        }
        private readonly string url = "https://gnews.io/api/v4/top-headlines?token=fc18f322740047ac0a178d5ea0b45ed4&lang=en";
        private readonly HttpClient client = new HttpClient();
        public NewsViewModel()
        {
            GetNewsContent();
        }
        public async Task NAvigatetoWebPage(ArticlesItems articlesItems)
        {

        }

        public async Task GetNewsContent()
        {
            //    NewsResponse newsResponse = null;
            var res = await client.GetStringAsync(url);
            var newsResponse = JsonConvert.DeserializeObject<NewsResponse>(res);
            if (newsResponse != null)
            {
                foreach (var item in newsResponse.articles)
                {
                    ArticlesItems articles = new ArticlesItems();
                    {
                        articles.description = item.description;
                        articles.WebUrl = item.url;
                        NewsList.Add(articles);
                    };
                }
            }
        }

        private List<ArticlesItems> newsList = new List<ArticlesItems>();
        public List<ArticlesItems> NewsList
        {
            get { return newsList; }
            set { newsList = value; OnPropertyChanged(nameof(NewsList)); }
        }

        public class ArticlesItems
        {
            public string description { get; set; }
            public string WebUrl { get; set; }
        }
    }
}


