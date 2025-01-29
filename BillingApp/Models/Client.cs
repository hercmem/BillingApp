using System;
using System.Collections.Generic;

namespace BillingApp.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string Afm { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Phone PhoneNumberNavigation { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
