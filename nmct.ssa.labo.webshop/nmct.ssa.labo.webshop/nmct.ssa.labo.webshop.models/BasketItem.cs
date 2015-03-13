using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
        public int Amount { get; set; }

        public double TotalPrice { get; set; }
        
        public DateTime Timestamp { get; set; }
        public bool Available { get; set; }
    }
}