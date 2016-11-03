using System.Collections.Generic;
using Umbraco.Web.PublishedContentModels;

namespace Web.Models
{
    public class GalleryResult : Pager
    {
        public List<GalleryItem> Items { get; set; }

        public string Title { get; set; }
    }
}
