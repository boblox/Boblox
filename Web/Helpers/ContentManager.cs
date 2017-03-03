using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using umbraco;
using Umbraco.Core;
using Umbraco.Core.Models;
using Web.Resources;
using Umbraco.Web;
using Umbraco.Web.PublishedContentModels;
using Web.Models;

namespace Web.Helpers
{
    /// <summary>
    /// Content helper
    /// </summary>
    public static class ContentManager
    {
        private static readonly UmbracoHelper UmbracoHelper = new UmbracoHelper(UmbracoContext.Current);

        #region Helpers

        private static DateTime GetNewsItemPublishDate(NewsItem item)
        {
            return item.PublishDate != default(DateTime) ? item.PublishDate : item.CreateDate;
        }

        private static IEnumerable<NewsItem> SortNews(this IEnumerable<NewsItem> items)
        {
            return items
                //.OrderByDescending(i => i.Parent<NewsByYear>().Name)
                .OrderByDescending(GetNewsItemPublishDate);
        }

        #endregion

        public static NewsResult GetNewsItems(int overviewPageId, int year, int page, int itemsPerPage)
        {
            var home = UmbracoHelper.TypedContentAtRoot().First();
            var overview = home.FirstChild(i => i.Id == overviewPageId)
                .OfType<NewsOverview>();
            var items = new List<NewsItem>();
            var totalPagesCount = 1;
            if (overview != null)
            {
                //Filter by year
                if (year == Consts.NewsConfig.YearAllInt)
                {
                    items = overview.Children<NewsItem>().ToList();
                }
                else
                {
                    items = overview.Children<NewsItem>()
                        .Where(i => GetNewsItemPublishDate(i).Year == year)
                        .ToList();
                }

                //Sort items
                items = items.SortNews().ToList();

                //Filter by page 
                totalPagesCount = (int)Math.Ceiling(((double)items.Count / itemsPerPage));
                if (page != Consts.NewsConfig.PageAllInt)
                {
                    items = items.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
                }
            }

            return new NewsResult
            {
                Items = items,
                Page = page,
                TotalPages = totalPagesCount
            };
        }

        public static List<NewsItem> GetLatestNews(int overviewPageId, int count)
        {
            return GetNewsItems(
                overviewPageId,
                Consts.NewsConfig.YearAllInt,
                1,
                count).Items;
        }

        //public static GalleryResult GetGalleryItems(int year, int page, int itemsPerPage)
        //{
        //    var home = UmbracoHelper.TypedContentAtRoot().First();
        //    var overview = home.FirstChild<GalleryOverview>();
        //    var items = new List<GalleryItem>();
        //    var totalPagesCount = 0;
        //    if (overview != null)
        //    {
        //        //Filter by year
        //        if (year == Consts.GalleryConfig.YearAllInt)
        //        {
        //            items = overview.Children<GalleryItem>().ToList();
        //        }
        //        else
        //        {
        //            items = overview.Children<GalleryItem>().Where(i => i.Year == year).ToList();
        //        }

        //        items = items
        //            .OrderByDescending(i => i.Year)
        //            .ThenByDescending(i => i.CreateDate)
        //            .ToList();

        //        //Filter by page 
        //        totalPagesCount = (int)Math.Ceiling(((double)items.Count / itemsPerPage));
        //        if (page != Consts.GalleryConfig.PageAllInt)
        //        {
        //            items = items.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
        //        }
        //    }

        //    return new GalleryResult
        //    {
        //        Title = overview.GetTitle(),
        //        Items = items,
        //        Page = page,
        //        TotalPages = totalPagesCount
        //    };
        //}

        public static List<Tuple<int, string>> GetNewsYears(int overviewPageId)
        {
            var home = UmbracoHelper.TypedContentAtRoot().First();
            var overview = home.FirstChild(i => i.Id == overviewPageId);
            var years = overview.Children<NewsItem>()
                .Select(GetNewsItemPublishDate)
                .DistinctBy(i => i.Year)
                .OrderByDescending(i => i)
                .Select(i => Tuple.Create(Convert.ToInt32(i.Year), $"{i.Year} {Localization.Year}"))
                .ToList();
            years.Insert(0, Tuple.Create(Consts.NewsConfig.YearAllInt, Localization.YearAll));
            return years;
        }

        //public static List<Tuple<int, string>> GetGalleryYears()
        //{
        //    var home = UmbracoHelper.TypedContentAtRoot().First();
        //    var overview = home.FirstChild<GalleryOverview>();
        //    var years = overview.Children<GalleryItem>()
        //        .Select(i => i.Year)
        //        .Distinct()
        //        .OrderByDescending(i => i)
        //        .Select(i => Tuple.Create(Convert.ToInt32(i), $"{i} {Localization.Year}"))
        //        .ToList();
        //    years.Insert(0, Tuple.Create(Consts.GalleryConfig.YearAllInt, Localization.YearAll));
        //    return years;
        //}

        public static IPublishedContent GetPage(int pageId)
        {
            return UmbracoHelper.TypedContent(pageId);
        }
    }
}