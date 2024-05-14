using System;
using System.Collections.Generic;

namespace AccoSystem.DataLayer;

public partial class Customer
{
    public string FullName { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string? Email { get; set; }

    public string? Addrese { get; set; }

    public int CustomerId { get; set; }

    public virtual ICollection<Accounting> Accountings { get; set; } = new List<Accounting>();
}
