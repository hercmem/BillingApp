using System;
using System.Collections.Generic;

namespace BillingApp.Models;

public partial class Call
{
    public int CallId { get; set; }

    public string Description { get; set; } = null!;
}
