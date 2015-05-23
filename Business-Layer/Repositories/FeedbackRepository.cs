using nmct.ssa.labo.webshop.businesslayer.Context;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class FeedbackRepository : GenericRepository<PersonForm, ApplicationDbContext>, IFeedbackRepository
    {
        public override PersonForm Insert(PersonForm entity)
        {
            ModeUnchanged(entity);
            return base.Insert(entity);
        }

        public override void Update(PersonForm entityToUpdate)
        {
            ModeUnchanged(entityToUpdate);
            base.Update(entityToUpdate);
        }

        private void ModeUnchanged(PersonForm entityToUpdate)
        {
            context.Entry<Mode>(entityToUpdate.Mode).State = System.Data.Entity.EntityState.Unchanged;
        }

        public List<PersonForm> GetFeedbackFrom(string mode)
        {
            return context.PersonForm
                .Where(p => p.Mode.Name.Equals(mode) && p.Checked == false)
                .ToList<PersonForm>();
        }

        public override PersonForm GetItemByID(object id)
        {
            return context.PersonForm
                .Include("Mode")
                .Where(p => p.Id == (int)id)
                .SingleOrDefault<PersonForm>();
        }
    }
}
