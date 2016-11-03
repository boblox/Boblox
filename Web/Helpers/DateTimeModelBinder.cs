using System;
using System.Globalization;
using System.Web.Mvc;
using Web.Resources;

namespace Web.Helpers
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (!string.IsNullOrEmpty(displayFormat) && value != null)
            {
                DateTime date;
                displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                // use the format specified in the DisplayFormat attribute to parse the date
                if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    //TODO: ozo; Here should be this. This is very bad quick fix
                    //bindingContext.ModelState.SetModelValue(bindingContext.ModelName,
                    //    new ValueProviderResult(date, value.AttemptedValue, CultureInfo.CurrentCulture));
                    bindingContext.ModelState.SetModelValue(bindingContext.ModelName,
                        new ValueProviderResult(value.AttemptedValue, value.AttemptedValue, CultureInfo.CurrentCulture));
                    return date;
                }
                else
                {
                    bindingContext.ModelState.AddModelError(
                        bindingContext.ModelName,
                        string.Format(Localization.DateTimeInvalid, value.AttemptedValue)
                    );
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
