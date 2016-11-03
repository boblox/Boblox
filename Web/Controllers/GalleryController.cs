using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedContentModels;
using Web.Helpers;
using GalleryResult = Web.Models.GalleryResult;

namespace Web.Controllers
{
    public class GalleryController : SurfaceController
    {
        [HttpPost]
        public ActionResult Index(int year, int page, int itemsPerPage)
        {
            //System.Threading.Thread.Sleep(1000);
            var model = ContentManager.GetGalleryItems(year, page, itemsPerPage);
            return PartialView("~/Views/Partials/Gallery/GalleryList.cshtml", model);
        }
    }
}
