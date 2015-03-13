using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.ViewModels
{
    public class UploadVM
    {
        public Device Device { get; set; }
        public string Id { get; set; }
        public string Image { get; set; }
    }
}