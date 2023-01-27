using Microsoft.Extensions.Options;
using Pokedex.Business.Core.Auth;
using Pokedex.Business.Core.Notifications;
using Pokedex.Business.Repositories;

namespace Pokedex.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotifier _notifier;
        private readonly AuthSettings _authSettings;

        public AuthenticationService(IUserRepository userRepository, INotifier notifier, IOptions<AuthSettings> options)
        {
            _userRepository = userRepository;
            _notifier = notifier;
            _authSettings = options.Value;
        }

        public async Task<string?> LoginAsync(LoginModel login)
        {
            var user = await _userRepository.GetByUsername(login.Username);

            if (user == null || user.Password != CryptographHelper.EncryptToMD5(login.Password))
            {
                _notifier.Notify("Usuario ou senha incorreto.");
                return null;
            }

            return JwtHelper.GenerateTokenJwt(login.Username, _authSettings);
        }
    }
}
