using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //DomainBus
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
