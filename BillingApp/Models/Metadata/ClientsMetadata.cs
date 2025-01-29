using System.ComponentModel.DataAnnotations;

namespace BillingApp.Models.Metadata
{
    public partial class ClientsMetadata
    {

        [Display(Name = "Full Name")]
        public string UserId { get; set; } = null!;

        [Display(Name = "AFM")]
        public string Afm { get; set; } = null!;

        [Display(Name = "Phone Number")]
        public string PhoneNumberNavigation { get; set; } = null!;
    }
}
