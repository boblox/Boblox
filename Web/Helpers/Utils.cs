using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.PublishedContentModels;

namespace Web.Helpers
{
    public static class Utils
    {
        public static string GenerateId(string prefix)
        {
            return $"{prefix}_{Guid.NewGuid().ToString("N")}";
        }

        public static string GetTitle(this IPublishedContent content)
        {
            var pageTitle = content.Name;
            if (content is ITitleComponent)
            {
                var title = ((ITitleComponent)content).Title;
                pageTitle = !string.IsNullOrEmpty(title) ? title : pageTitle;
            }
            return pageTitle;
        }

        public static IEnumerable<PreValue> GetDataTypePreValues(int dataTypeId)
        {
            var service = ApplicationContext.Current.Services.DataTypeService;
            return service.GetPreValuesCollectionByDataTypeId(dataTypeId)
                .FormatAsDictionary()
                .Values;
        }

        public static void SendEmail(string emailFrom, string emailTo, string subject, string body)
        {
            var message = new MailMessage
            {
                From = new MailAddress(emailFrom, emailFrom),
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            message.To.Add(emailTo);

            var smtpClient = new SmtpClient();
            smtpClient.Send(message);
        }
    }
}
