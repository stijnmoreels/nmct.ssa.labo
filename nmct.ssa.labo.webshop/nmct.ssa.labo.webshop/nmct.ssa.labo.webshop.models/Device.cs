using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.models
{
    public class Device
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double BuyPrice { get; set; }
        [Required]
        public double RentPrice { get; set; }
        [Required]
        public int Stock { get; set; }
        public string Image  { get; set; }
        public List<OS> OS { get; set; }
        public List<FrameWork> FrameWorks { get; set; }

        public Device()
        {
            this.OS = new List<OS>();
            this.FrameWorks = new List<FrameWork>();
        }

        public string GetStringOS()
        {
            string hidden = "";
            foreach (OS os in this.OS)
                hidden += os.Id + ";";
            return hidden;
        }
        
        public string GetStringFrame()
        {
            string hidden = "";
            foreach (FrameWork frame in this.FrameWorks)
                hidden += frame.Id + ";";
            return hidden;
        }
    }
}