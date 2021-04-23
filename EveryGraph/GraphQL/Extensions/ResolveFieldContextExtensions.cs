using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveryGraph.GraphQL.Extensions
{
    public static class ResolveFieldContextExtensions
    {
        public static int? ResolveArgumentTop(this IResolveFieldContext context)
        {
            int? result = context.GetArgument<int>("top");
            return result;
        }
    }
}
