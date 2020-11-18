using System;
using System.Collections.Generic;
using System.Text;

namespace DemoAPP.ViewModels
{
  public  class NewsResponse

    {
        public int? totalArticles { get; set; }
        public IList<Articles> articles { get; set; }

    }

    public class Source
    {
        public string name { get; set; }
        public string url { get; set; }

    }
    public class Articles
    {
        public string title { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string image { get; set; }
        public string publishedAt { get; set; }
        public Source source { get; set; }

    }

}
