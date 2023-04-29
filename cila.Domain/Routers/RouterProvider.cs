using cila;
using cila.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace cila.Domain.Routers
{
    public class RouterProvider
    {
        private readonly IServiceProvider serviceProvider;

        public RouterProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICilaRouter GetRouter(RouterContext context)
        {
            switch (context.Stretagy)
            {
                case RoutingStrategy.Random:
                    return serviceProvider.GetService<RandomRouter>();
                case RoutingStrategy.Optimal:
                    return serviceProvider.GetService<EfficientRouter>();
                default:
                    return serviceProvider.GetService<RandomRouter>();
            }
        }
    }

    

    public class RouterContext
    {
        public RoutingStrategy Stretagy {get;set;}

        public static RouterContext Default = new RouterContext{ Stretagy = RoutingStrategy.Random};
    }

    public enum RoutingStrategy
    {
        Chepest,
        Optimal,
        Fastest,
        Random
    }
}