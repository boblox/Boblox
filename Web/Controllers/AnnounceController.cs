//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using Umbraco.Web;
//using Umbraco.Web.Mvc;
//using Umbraco.Web.PublishedContentModels;
//using Web.Helpers;
//using NewsResult = Web.Models.NewsResult;

//namespace Web.Controllers
//{
//    public class AnnounceController : SurfaceController
//    {
//        #region Helpers

//        private static List<NewsItem> SortNews(IEnumerable<NewsItem> items)
//        {
//            //Order by NewsByYear node, then by publish or create date
//            return items.OrderByDescending(i => i.Parent<NewsByYear>().Name)
//                    .ThenByDescending(i => i.PublishDate != default(DateTime) ? i.PublishDate : i.CreateDate)
//                    .ToList();
//        }

//        #endregion

//        #region Actions

//        //[ChildActionOnly]
//        //public JsonResult Index(string year, string category, int page)
//        [HttpPost]
//        public ActionResult Index(int year, int category, int page)
//        {
//            var home = Umbraco.TypedContentAtRoot().First();
//            var overview = home.FirstChild<NewsOverview>();
//            var items = new List<NewsItem>();
//            int totalPagesCount = 0;
//            if (overview != null)
//            {
//                //Filter by year
//                if (year == Consts.NewsConfig.YearAllInt)
//                {
//                    items = overview.Children.SelectMany(i => i.Children<NewsItem>()).ToList();
//                }
//                else
//                {
//                    var newsByYear = overview.Children.FirstOrDefault(i => i.Name == year.ToString());
//                    if (newsByYear != null)
//                    {
//                        items = newsByYear.Children<NewsItem>().ToList();
//                    }
//                }

//                //Filter by category
//                if (category == Consts.NewsConfig.NewsCategoryAnnounceInt)
//                {
//                    items = items.Where(i => i.IsAnnounce).ToList();
//                }
//                else if (category != Consts.NewsConfig.NewsCategoryAllInt)
//                {
//                    var newsCategories = Utils.GetDataTypePreValues(NewsItem.GetModelPropertyType(i => i.Category).DataTypeId).ToList();
//                    var categoryStr = newsCategories.First(i => i.Id == category).Value;
//                    items = items.Where(i =>
//                        ((string)i.Category).Equals(categoryStr, StringComparison.InvariantCultureIgnoreCase))
//                        .ToList();
//                }

//                //Sort items
//                items = SortNews(items);

//                //Filter by page 
//                totalPagesCount = (int)Math.Ceiling(((double)items.Count / Consts.NewsConfig.NewsPerPage));
//                if (page != Consts.NewsConfig.PageAllInt)
//                {
//                    items = items.Skip((page - 1) * Consts.NewsConfig.NewsPerPage).Take(Consts.NewsConfig.NewsPerPage).ToList();
//                }
//            }

//            var model = new NewsResult
//            {
//                Items = items,
//                Page = page,
//                TotalPages = totalPagesCount
//            };
//            //System.Threading.Thread.Sleep(1000);
//            return PartialView("NewsList", model);
//        }

//        [ChildActionOnly]
//        public ActionResult GetRelatedNews(int id)
//        {
//            var home = Umbraco.TypedContentAtRoot().First();
//            var overview = home.FirstChild<NewsOverview>();
//            var items = SortNews(overview.Children.SelectMany(i => i.Children<NewsItem>()));
//            var index = items.FindIndex(i => i.Id == id);
//            NewsItem prev = null, next = null;
//            if (index >= 1)
//            {
//                next = items[index - 1];
//            }
//            if (index <= items.Count - 2)
//            {
//                prev = items[index + 1];
//            }
//            var model = new[] { prev, next };
//            return PartialView("RelatedNews", model);
//        }

//        #endregion
//    }
//}
