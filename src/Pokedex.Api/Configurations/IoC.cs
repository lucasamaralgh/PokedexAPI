using Pokedex.Business.Repositories;
using Pokedex.Business.Services;
using Pokedex.Infra.Repositories;

namespace Pokedex.Api.Configurations
{
    public static class IoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Se chamar a interface, chama a classe da service
            services.AddScoped<IPokedexService, PokedexService>(); 
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
