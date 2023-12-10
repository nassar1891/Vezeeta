using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VezeetaAPI.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
