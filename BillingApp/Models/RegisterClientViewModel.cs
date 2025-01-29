namespace BillingApp.Models
{
    public class RegisterClientViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Property { get; set; } = "client"; // Fixed value
        public string Afm { get; set; }
        public string PhoneNumber { get; set; }
        public string ProgramName { get; set; }
    }
}
