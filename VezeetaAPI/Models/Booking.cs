using System;
using System.Collections.Generic;

namespace VezeetaAPI.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? ScheduleId { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Schedule? Schedule { get; set; }
}
