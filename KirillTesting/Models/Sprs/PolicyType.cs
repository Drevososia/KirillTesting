using System;
using System.Collections.Generic;

namespace KirillTesting.Models.Sprs;

public partial class PolicyType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}
