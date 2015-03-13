using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo.webshop.ViewModels
{
    public class CreateDeviceVM
    {
        public Device Device { get; set; }
        public SelectList Os { get; set; }
        public SelectList FrameWorks { get; set; }

        public SelectList SelectedOs { get; set; }
        public SelectList SelectedFrameWorks { get; set; }

        public int SelectedOsId { get; set; }
        public int SelectedFrameId { get; set; }
        
        public string HiddenOS { get; set; }
        public string HiddenFrame { get; set; }

        public CreateDeviceVM()
        {
            this.Device = new Device();
        }

        public string btnAddOS { get; set; }
        public string btnAddFrame { get; set; }
        public string btnSend { get; set; }
    }
}