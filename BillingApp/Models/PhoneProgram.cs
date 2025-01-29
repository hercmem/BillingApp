using System;
using System.Collections.Generic;

namespace BillingApp.Models;

public partial class PhoneProgram
{
    public string ProgramName { get; set; } = null!;

    public string Benfits { get; set; } = null!;

    public decimal Charge { get; set; }

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
