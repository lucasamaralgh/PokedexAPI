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
            services.AddScoped<IPokedexService, PokedexService>(); // 1 objeto por request
            return services;

            //services.AddAddTransient<IPokedexService, PokedexService>(); // 10 objetos 
            //services.AddSingleton<IPokedexService, PokedexService>(); -> 1 objeto enquanto a aplicação estiver no ar
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            return services;
        }
    }
}
