using Pokedex.Business.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
    }
}
