using Pokedex.Business.Core.Auth;
using Pokedex.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetByUsername(string username)
        {
            return Task.FromResult(new User()
            {
                Username = username,
                Password = "81dc9bdb52d04dc20036dbd8313ed055"
            });
        }
    }
}
