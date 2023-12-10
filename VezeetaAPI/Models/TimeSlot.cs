using System;
using System.Collections.Generic;

namespace VezeetaAPI.Models;

public partial class TimeSlot
{
    public int Id { get; set; }

    public int? ScheduleId { get; set; }

    public TimeSpan? Time { get; set; }

    public virtual Schedule? Schedule { get; set; }
}
