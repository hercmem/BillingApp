using System.ComponentModel.DataAnnotations;

namespace BillingApp.Models.Metadata
{
    public partial class PhonesMetadata
    {
        [Display(Name = "Program Name")]
        public string ProgramNameNavigation { get; set; } = null!;
    }
}
