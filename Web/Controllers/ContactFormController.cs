using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using Logic.Models;
using Logic.Resources;
using Umbraco.Core.Logging;
using Umbraco.Web.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
    public class ContactFormController : SurfaceController
    {
        #region Actions

        [HttpPost]
        public JsonResult Send(ContactForm model)
        {
            var success = true;
            try
            {
                if (!ModelState.IsValid)
                {
                    success = false;
                }

                var body = GetMailBody(model);

                Utils.SendEmail(model.EmailFrom,
                    model.EmailTo,
                    Localization.ContactFormEmailSubject,
                    body);
            }
            catch (Exception e)
            {
                LogHelper.Error(GetType(), e.ToString(), e);
                success = false;
            }
            return Json(new { success });
        }

        [ChildActionOnly]
        public ActionResult Index(string emailFrom, string emailTo)
        {
            return PartialView("~/Views/Partials/Common/ContactForm.cshtml", new ContactForm { EmailFrom = emailFrom, EmailTo = emailTo });
        }

        #endregion

        #region Helpers

        private string GetMailBody(ContactForm model)
        {
            var doc = new HtmlDocument();
            doc.Load(Server.MapPath("~/Email/ContactForm.html"));
            var builder = new StringBuilder(doc.DocumentNode.SelectSingleNode("//body").WriteTo());

            ReplacePlaceholder(builder, "NameSurnameTitle", Localization.ContactFormNameSurname);
            ReplacePlaceholder(builder, "NameSurname", model.NameSurname);

            ReplacePlaceholder(builder, "ClientEmailTitle", Localization.ContactFormClientEmail);
            ReplacePlaceholder(builder, "ClientEmail", model.ClientEmail);

            ReplacePlaceholder(builder, "DescriptionTitle", Localization.ContactFormDescription);
            ReplacePlaceholder(builder, "Description", model.Description);

            return builder.ToString();
        }

        private void ReplacePlaceholder(StringBuilder builder, string key, string value)
        {
            key = $"[{key}]";
            value = HttpUtility.HtmlEncode(value);
            builder.Replace(key, value);
        }

        #endregion
    }
}
