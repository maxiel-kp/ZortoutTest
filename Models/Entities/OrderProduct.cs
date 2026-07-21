using System;
using System.Collections.Generic;

namespace ZortouTest.Models.Entities;

public partial class OrderProduct
{
    public Guid? OrderNumber { get; set; }

    public int? ProductCode { get; set; }

    public decimal? PricePerUnit { get; set; }

    public int? Quantity { get; set; }
}
