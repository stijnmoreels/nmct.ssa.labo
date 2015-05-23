using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces
{
    public interface IFeedbackRepository : IGenericRepository<PersonForm>
    {
        List<PersonForm> GetFeedbackFrom(string mode);
    }
}
