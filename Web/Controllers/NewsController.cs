using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedContentModels;
using Web.Helpers;
using NewsResult = Web.Models.NewsResult;

namespace Web.Controllers
{
    public class NewsController : SurfaceController
    {
        #region Actions

        //[ChildActionOnly]
        [HttpPost]
        public ActionResult Index(int year, int page, int itemsPerPage)
        {
            //System.Threading.Thread.Sleep(1000);

            var model = ContentManager.GetNewsItems(year, page, itemsPerPage);
            return PartialView("~/Views/Partials/News/NewsList.cshtml", model);
        }

        #endregion
    }
}
