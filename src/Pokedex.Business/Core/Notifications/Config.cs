using Microsoft.Extensions.DependencyInjection;

namespace Pokedex.Business.Core.Notifications
{
    public static class Config
    {
        public static IServiceCollection AddSmartNotification(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();
            return services;
        }
    }
}