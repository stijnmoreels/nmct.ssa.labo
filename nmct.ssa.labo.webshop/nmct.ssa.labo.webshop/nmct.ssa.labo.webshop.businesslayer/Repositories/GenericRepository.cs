using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class GenericRepository<T, C> : IGenericRepository<T> where T : class where C : DbContext, new()
    {

        //internal ApplicationDbContext context;
        internal C context;
        internal DbSet<T> dbSet;

        public GenericRepository()
        {
            //this.context = new ApplicationDbContext();
            this.context = new C();
            this.dbSet = context.Set<T>();
        }

        //public GenericRepository(ApplicationDbContext context)
        public GenericRepository(C context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> All()
        {
            return dbSet;
        }

        public virtual T GetItemByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual T Insert(T entity)
        {
            return dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
