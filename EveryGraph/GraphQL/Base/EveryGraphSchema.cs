using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EveryGraph.GraphQL.Base
{
    public class EveryGraphSchema : Schema
    {
        public EveryGraphSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<EveryGraphQuery>();
        }
    }
}
