using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab7.Models
{
    public class Agent
    {
        public int AgentID { get; set; }
        [Required]
        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }
        public string Address { get; set; }

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; }
    }
}