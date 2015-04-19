using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> All();
        void Delete(object id);
        void Delete(T entityToDelete);
        T GetItemByID(object id);
        T Insert(T entity);
        void Update(T entityToUpdate);
        void SaveChanges();
    }
}