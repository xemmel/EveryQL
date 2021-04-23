using Microsoft.Extensions.DependencyInjection;
using System;

namespace KaggleReader.Library.DI
{
    public static class ServiceFactory
    {
        public static T GetService<T>(Action<IServiceCollection>? action = null)
        {
            var services = new ServiceCollection();
            services.AddKaggleReader(action: action);
            return services.BuildServiceProvider().GetRequiredService<T>();
        }
    }
}
