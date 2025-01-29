using System;
using System.Collections.Generic;

namespace BillingApp.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public decimal Costs { get; set; }

    public bool IsPaid { get; set; }

    public virtual Phone PhoneNumberNavigation { get; set; } = null!;
}
