using System.ComponentModel.DataAnnotations;

namespace BillingApp.Models
{
    public class ChangeClientProgramViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string ProgramName { get; set; }
    }
}
