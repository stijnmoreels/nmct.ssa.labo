using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public class CultureService : ICultureService
    {
        private ICultureRepository cultureRepository = null;

        public CultureService(ICultureRepository cultureRepository)
        {
            this.cultureRepository = cultureRepository;
        }

        public List<Culture> GetAllAvailableCultures()
        {
            return cultureRepository.All().ToList<Culture>();
        }

        public Culture GetCultureFromId(int id)
        {
            return cultureRepository.GetItemByID(id);
        }
    }
}
