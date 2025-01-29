namespace BillingApp.Models
{
    public class ClientBillViewModel
    {
        public int BillId { get; set; }
        public string ProgramName { get; set; }
        public decimal Costs { get; set; }

        public bool IsPaid { get; set; }
    }
}
