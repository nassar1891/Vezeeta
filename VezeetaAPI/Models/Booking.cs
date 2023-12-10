using System;

namespace VezeetaAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? ScheduleId { get; set; }
        public bool DiscountApplied { get; set; }
        public decimal DiscountAmount { get; set; }
        public string? CouponCode { get; set; }
        public BookingState State { get; set; }
        public virtual Patient Patient { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
    }

    public enum BookingState
    {
        Pending,
        Confirmed,
        Denied,
    }
}
