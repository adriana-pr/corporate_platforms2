using System;
using System.Collections.Generic;

namespace lab2.Models;

public partial class Amountofbill
{
    public int Id { get; set; }

    public int ContributorId { get; set; }

    public double Amount { get; set; }

    public virtual Contributor Contributor { get; set; } = null!;
}
