using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab7.Models.ViewModels
{
    public class BestItemViewModel
    {
        public Item Item { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OrderCount { get; set; }
    }
}