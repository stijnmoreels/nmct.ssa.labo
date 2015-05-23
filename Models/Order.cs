using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.models
{
    public class Order
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public int CourierId { get; set; }
        public virtual Courier Courier { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public List<OrderLine> Orders { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Deleted { get; set; }
    }
}