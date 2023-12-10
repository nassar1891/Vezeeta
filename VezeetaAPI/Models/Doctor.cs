using System;
using System.Collections.Generic;

namespace VezeetaAPI.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string? Specialization { get; set; }

    public decimal? Price { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
