using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.models
{
    public class TranslatedDevice
    {
        [Key, Column(Order = 0), ForeignKey("Device")]
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
        [Key, Column(Order = 1), ForeignKey("Culture")]
        public int CultureId { get; set; }
        public virtual Culture Culture { get; set; }
        [Required(ErrorMessageResourceName="txtDescriptionRequired", ErrorMessageResourceType = typeof(Properties.General.General))]
        public string Description { get; set; }
    }
}
