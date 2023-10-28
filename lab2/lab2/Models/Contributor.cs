using System;
using System.Collections.Generic;

namespace lab2.Models;

public partial class Contributor
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string? PassportSerial { get; set; }

    public string? PassportNumber { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Amountofbill> Amountofbills { get; set; } = new List<Amountofbill>();

    public virtual ICollection<Bankdeposit> Bankdeposits { get; set; } = new List<Bankdeposit>();
}
