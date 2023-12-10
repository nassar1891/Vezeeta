using System;
using System.Collections.Generic;

namespace VezeetaAPI.Models;

public partial class ApplicationUser
{
    public int Id { get; set; }

    public byte[]? Image { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }
    
    public string? PasswordHash { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
