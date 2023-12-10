using System;
using System.Collections.Generic;

namespace VezeetaAPI.Models;

public partial class Patient
{
    public int Id { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
}
