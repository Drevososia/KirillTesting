using KirillTesting.Models.Sprs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KirillTesting.Models;

public partial class Policy
{
    public int Id { get; set; }

    public int Type { get; set; }

    public string? Serial { get; set; }

    public string? Number { get; set; }

    [StringLength(16, MinimumLength = 16, ErrorMessage ="ЕНП обязателен для указания")]
    public string Enp { get; set; }
    public int PersonId { get; set; }
    public virtual PolicyType TypeNavigation { get; set; } = null!;
    public virtual Person Person { get; set; }
}
