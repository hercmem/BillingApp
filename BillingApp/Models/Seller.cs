using System;
using System.Collections.Generic;

namespace BillingApp.Models;

public partial class Seller
{
    public int SellerId { get; set; }

    public int UserId { get; set; }

    public virtual AppUser User { get; set; } = null!;
}
