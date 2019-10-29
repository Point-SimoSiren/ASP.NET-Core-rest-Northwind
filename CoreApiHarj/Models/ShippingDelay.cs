using System;
using System.Collections.Generic;

namespace CoreApiHarj.Models
{
    public partial class ShippingDelay
    {
        public int OrderId { get; set; }
        public int? ShippingDelay1 { get; set; }
    }
}
