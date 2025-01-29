using System.ComponentModel.DataAnnotations;

namespace BillingApp.Models.Metadata
{
    public partial class BillsMetadata
    {
        [Display(Name = "Phone Number")]
        public string PhoneNumberNavigation { get; set; } = null!;

        [Display(Name = "Cost")]
        public string Costs { get; set; } = null!;
    }
}
