﻿using System;
using System.Collections.Generic;

namespace VezeetaAPI.Models;

public partial class Coupon
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public decimal? Discount { get; set; }

    public DateTime? ExpiryDate { get; set; }
}
