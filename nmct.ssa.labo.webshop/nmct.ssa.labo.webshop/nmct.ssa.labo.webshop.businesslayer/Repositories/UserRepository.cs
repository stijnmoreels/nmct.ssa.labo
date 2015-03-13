using nmct.ssa.labo.webshop.businesslayer.Context;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser, ApplicationDbContext>, IUserRepository
    {
        public ApplicationUser GetUserByName(string user)
        {
            return context.Users
                .Where(u => u.UserName.Equals(user))
                .SingleOrDefault<ApplicationUser>();
        }
    }
}
