using System;
using System.Collections.Generic;

namespace BillingApp.Models;

public partial class Phone
{
    public string PhoneNumber { get; set; } = null!;

    public string ProgramName { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual PhoneProgram ProgramNameNavigation { get; set; } = null!;
}
