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
using MicroRabbit.Transfer.Application._1_Interfaces;
using MicroRabbit.Transfer.Application._2_Services;
using MicroRabbit.Transfer.Data._1_Context;
using MicroRabbit.Transfer.Data._2_Repository;
using MicroRabbit.Transfer.Domain._3_Events;
using MicroRabbit.Transfer.Domain._4_EventHandlers;
using MicroRabbit.Transfer.Domain._5_Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //DomainBus
            //Dessa forma os EventHandlers podem sofrer uma injeção de dependencia
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });


            //Subscriptions
            services.AddTransient<TransferEventHandler>();


            //Domain Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();


            //Application Layer
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();

            //Data Layer
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();

            //Context
            services.AddTransient<BankingDbContext>();
            services.AddTransient<TransferDbContext>();
        }
    }
}