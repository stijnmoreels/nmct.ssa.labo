using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.models
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}