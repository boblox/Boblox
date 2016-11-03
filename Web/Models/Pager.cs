namespace Web.Models
{
    public class Pager //: RenderModel
    {
        //public NewsPager()
        //    : base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
        //{
        //}

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
