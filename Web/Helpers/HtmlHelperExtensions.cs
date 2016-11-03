using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Logic.Resources;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedContentModels;

namespace Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

            var enumValues = Enum.GetValues(enumType).Cast<TEnum>();

            var items = from enumValue in enumValues
                        select new SelectListItem
                        {
                            Text = GetResourceValueForEnumValue(enumValue),
                            Value = enumValue.ToString(),
                            Selected = enumValue.Equals(metadata.Model)
                        };

            return html.DropDownListFor(expression, items);
        }

        private static string GetResourceValueForEnumValue<TEnum>(TEnum enumValue)
        {
            var result = null as string;
            var displayAttr = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttributes(false)
                .OfType<DisplayAttribute>()
                .LastOrDefault();
            if (displayAttr != null)
            {
                result = displayAttr.GetName();
            }
            return result ?? enumValue.ToString();
        }

        public static MvcHtmlString GetInnerText(this HtmlHelper html, IHtmlString htmlText, int length = 0)
        {
            var helper = new UmbracoHelper();
            var text = helper.StripHtml(htmlText);
            return new MvcHtmlString(length == 0 ? text.ToString() : helper.Truncate(text, length).ToString());
        }

        public static MvcHtmlString Localize(this HtmlHelper html, string key, params object[] args)
        {
            var format = Localization.ResourceManager.GetString(key) ?? string.Empty;
            return new MvcHtmlString(string.Format(format, args));
        }

        public static MvcHtmlString GetThumbnailUrl(this HtmlHelper html, string url, int? width = null, int? height = null, ImageCropMode? cropMode = null)
        {
            return new MvcHtmlString(url.GetCropUrl(width, height, imageCropMode: ImageCropMode.Crop, furtherOptions: "&bgcolor=fff"));
        }
    }
}
