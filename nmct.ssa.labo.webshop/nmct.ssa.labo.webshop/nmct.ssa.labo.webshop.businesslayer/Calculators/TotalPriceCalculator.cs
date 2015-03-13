using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.businesslayer.Calculators
{
    public class TotalPriceCalculator
    {
        public static double CalculateTotalPrice(List<BasketItem> items)
        {
            double total = 0;
            items.ForEach(d => total += (d.Device.BuyPrice * d.Amount));
            return total;
        }

        public static double CalculateTotalPrice(Device device, int amount)
        {
            return device.BuyPrice * amount;
        }
    }
}