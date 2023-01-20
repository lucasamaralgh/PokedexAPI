using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
