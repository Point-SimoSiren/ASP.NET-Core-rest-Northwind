using System;
using System.Collections.Generic;

namespace CoreApiHarj.Models
{
    public partial class ConfectionsTmp
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public short? UnitsInStock { get; set; }
        public string ProductName { get; set; }
    }
}
