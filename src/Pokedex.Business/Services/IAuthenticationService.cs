using Pokedex.Business.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Services
{
    public interface IAuthenticationService
    {
        Task<string?> LoginAsync(LoginModel model);
    }
}
