using KaggleReader.Library.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KaggleReader.Library.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKaggleReader(this IServiceCollection services, Action<IServiceCollection>? action = null)
        {
            services
                    .AddSingleton<IJsonHandler, JsonHandler>()
                    .AddSingleton<IEurovisionHandler, EurovisionHandler>();
            action?.Invoke(services);
            return services;
        }

    }
}
