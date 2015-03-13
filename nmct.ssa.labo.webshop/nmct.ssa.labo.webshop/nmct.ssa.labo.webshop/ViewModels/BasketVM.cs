using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.ViewModels
{
    public class BasketVM
    {
        public List<BasketItem> Baskets { get; set; }
        public double TotalPrice { get; set; }
    }
}