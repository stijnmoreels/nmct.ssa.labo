using nmct.ssa.labo.webshop.businesslayer.Repositories;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository = null;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ApplicationUser GetAllUserValues(string user)
        {
            return this.userRepository.GetUserByName(user);
        }
    }
}
