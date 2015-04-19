using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.Controllers.ControllerInterface
{
    public interface IControllerService
    {
        ICatalogService CatalogService { get; set; }
    }
}
