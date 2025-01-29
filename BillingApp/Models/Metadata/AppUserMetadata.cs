using System.ComponentModel.DataAnnotations;

namespace BillingApp.Models.Metadata
{
    public partial class AppUserMetadata
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
    }
}
