using nmct.ssa.labo.webshop.businesslayer.Context;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class CultureRepository : GenericRepository<Culture, ApplicationDbContext>, ICultureRepository
    {
        
    }
}
