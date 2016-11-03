using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web.PublishedContentModels;

namespace Web.Models
{
    public class NewsResult : Pager
    {
        public List<NewsItem> Items { get; set; }
    }
}
