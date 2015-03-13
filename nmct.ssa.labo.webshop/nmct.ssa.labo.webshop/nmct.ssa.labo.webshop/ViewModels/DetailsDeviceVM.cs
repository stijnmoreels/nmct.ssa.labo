using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.ViewModels
{
    public class DetailsDeviceVM
    {
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int Amount { get; set; }
    }
}