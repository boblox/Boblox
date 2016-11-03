using System.Linq;
using System.Web.Mvc;
using Examine;
using UmbracoExamine;
using Examine.LuceneEngine.SearchCriteria;
using Examine.SearchCriteria;
using Lucene.Net.Search;
using Umbraco.Web.Mvc;

namespace Web.Controllers
{
    public class SearchController : SurfaceController
    {
        [HttpPost]
        public ActionResult Index(string text)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection["ExternalSearcher"];
            var searchCriteria = searcher.CreateSearchCriteria(BooleanOperation.Or);
            var searchFields = new[] {"nodeName", "metaTitle"};
            var query = searchCriteria.GroupedOr(searchFields, text.Fuzzy(0.8f)).Compile();
            var searchResults = searcher.Search(query).OrderByDescending(x => x.Score);
            return null;
        }
    }
}
