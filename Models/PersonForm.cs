using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.models
{
    public class PersonForm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Email { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Mode Mode { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; }
    }
}
