using Logic.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Logic.Models
{
    public class ContactForm
    {
        public string EmailFrom { get; set; }

        public string EmailTo { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        [Display(Name = "ContactFormNameSurname", ResourceType = typeof(Localization))]
        public string NameSurname { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        [Display(Name = "ContactFormClientEmail", ResourceType = typeof(Localization))]
        [EmailAddress(ErrorMessageResourceName = "ContactFormClientEmailInvalid", ErrorMessageResourceType = typeof(Localization), ErrorMessage = null)]
        public string ClientEmail { get; set; }

        ////[Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        //[Display(Name = "ContactFormClientPhone", ResourceType = typeof(Localization))]
        //[DataType(DataType.PhoneNumber)]
        //public string ClientPhone { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        [Display(Name = "ContactFormDescription", ResourceType = typeof(Localization))]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        ////[Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        //[Display(Name = "ContactFormHours", ResourceType = typeof(Localization))]
        //public string Hours { get; set; }

        //[Display(Name = "ContactFormIsPostProcessingNeeded", ResourceType = typeof(Localization))]
        //public bool IsPostProcessingNeeded { get; set; }

        //[Display(Name = "ContactFormIsStudioBookingNeeded", ResourceType = typeof(Localization))]
        //public bool IsStudioBookingNeeded { get; set; }
    }
}