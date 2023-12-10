using System;
using System.Collections.Generic;

namespace VezeetaAPI.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int? DoctorId { get; set; }

    public int? Days { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
