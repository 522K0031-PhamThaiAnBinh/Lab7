using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab7.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Unit Amount")]
        [DataType(DataType.Currency)]
        public decimal UnitAmount { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Item Item { get; set; }
    }
}