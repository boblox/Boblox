using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedContentModels;

namespace Web.Helpers
{
    public static class Utils
    {
        public static Random Random;

        static Utils()
        {
            Random = new Random((int)DateTime.Now.Ticks);
        }

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

        public static string GetImageNotFoundUrl(int width, int height)
        {
            return "/css/images/img-not-found.png"
                .GetCropUrl(width, height, null, null, null, ImageCropMode.Crop);
        }

        public static string GetRandomRotateStyle(double min, double max)
        {
            var length = Math.Abs(min) + Math.Abs(max);
            var degree = (Random.Next() % (length * 10)) / 10 - Math.Abs(min);
            var degreeStr = degree.ToString(CultureInfo.InvariantCulture);
            return $"transform: rotate({degreeStr}deg)";
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
