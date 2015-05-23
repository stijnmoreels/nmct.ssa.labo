using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Services.Interfaces
{
    public interface ICultureService
    {
        List<Culture> GetAllAvailableCultures();
        Culture GetCultureFromId(int id);
    }
}
