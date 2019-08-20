using MediatR;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Domain._2_Interfaces;
using MicroRabbit.Banking.Domain._3_Commands;
using MicroRabbit.Banking.Domain._4_CommandHandlers;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //DomainBus
            services.AddTransient<IEventBus, RabbitMQBus>();

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>,TransferCommandHandler>();


            //Application Layer
            services.AddTransient<IAccountService, AccountService>();

            //Data Layer
            services.AddTransient<IAccountRepository, AccountRepository>();

            //Context
            services.AddTransient<BankingDbContext>();
        }
    }
}
