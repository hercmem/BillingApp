using System.ComponentModel.DataAnnotations;

namespace BillingApp.Models.Metadata
{
    public partial class PhoneProgramsMetadata
    {
        [Display(Name = "Benefits")]
        public string Benfits { get; set; } = null!;
    }
}
