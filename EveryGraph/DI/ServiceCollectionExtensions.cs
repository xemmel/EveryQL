using EveryGraph.GraphQL.Base;
using GraphQL.Server;
using Microsoft.Extensions.DependencyInjection;

namespace EveryGraph.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEveryGraph(this IServiceCollection services)
        {
            services.AddSingleton<EveryGraphSchema>()
                    .AddGraphQL()
                    .AddSystemTextJson()
                    .AddGraphTypes(ServiceLifetime.Singleton);
            return services;
        }
    }
}
