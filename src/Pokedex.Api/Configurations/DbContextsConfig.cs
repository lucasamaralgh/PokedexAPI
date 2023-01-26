using Microsoft.EntityFrameworkCore;
using Pokedex.Infra;
using Pokedex.Infra.Interceptors;

namespace Pokedex.Api.Configurations
{
    public static class DbContextsConfig
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration["SqlServer:PokedexDb"];

            services.AddDbContext<EFDbContext>(options =>
            {
                options.UseSqlServer(connectionsString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(4, TimeSpan.FromSeconds(20), null);
                });

                options.AddInterceptors(new EfMetadataInterceptor());
            });

            return services;
        }
    }
}
