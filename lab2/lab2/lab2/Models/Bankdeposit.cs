using System;
using System.Collections.Generic;

namespace lab2.Models;

public partial class Bankdeposit
{
    public int Id { get; set; }

    public int ContributorId { get; set; }

    public DateOnly Date { get; set; }

    public double DepositAmount { get; set; }

    public virtual Contributor Contributor { get; set; } = null!;
}
