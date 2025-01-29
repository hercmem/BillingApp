using System;
using System.Collections.Generic;

namespace BillingApp.Models;

public partial class BillsCall
{
    public int BillId { get; set; }

    public int CallId { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Call Call { get; set; } = null!;
}
