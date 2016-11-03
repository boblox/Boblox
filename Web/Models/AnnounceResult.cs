using System.Collections.Generic;
using Umbraco.Web.PublishedContentModels;

namespace Web.Models
{
    public class AnnounceResult : Pager
    {
        public List<AnnounceItem> Items { get; set; }
    }
}
