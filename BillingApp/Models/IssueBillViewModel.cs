namespace BillingApp.Models
{
    public class IssueBillViewModel
    {
        public string PhoneNumber { get; set; }
        public string ProgramName { get; set; } // Retrieved from PhoneProgram
        public bool IsPaid { get; set; } = false;
        public decimal Cost { get; set; } // Maps to `Costs` in `Bill`
    }
}
