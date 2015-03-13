using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.models
{
    public class OS
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Device> Devices { get; set; }

        public OS()
        {

        }
    }
}