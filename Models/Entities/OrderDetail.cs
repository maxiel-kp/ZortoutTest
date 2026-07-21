using System;
using System.Collections.Generic;

namespace ZortouTest.Models.Entities;

public partial class OrderDetail
{
    public Guid OrderNumber { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }
}
