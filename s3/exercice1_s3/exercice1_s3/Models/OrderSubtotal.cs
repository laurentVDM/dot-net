﻿using System;
using System.Collections.Generic;

namespace exercice1_s3.Models;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}