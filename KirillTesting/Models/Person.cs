using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KirillTesting.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Patronymic { get; set; }

    [Range(3,120, ErrorMessage ="Возраст обязателен для указания")]
    public int Age { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? Gender { get; set; }

    public virtual Policy Policy { get; set; } = null!;
}
