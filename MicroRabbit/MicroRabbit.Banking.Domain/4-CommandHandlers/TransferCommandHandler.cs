using MediatR;
using MicroRabbit.Banking.Domain._3_Commands;
using MicroRabbit.Banking.Domain._5_Events;
using MicroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain._4_CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        
        private readonly IEventBus _eventBus;
        public TransferCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }   

        public async Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //publish para RabbitMQ
            _eventBus.Publish(new TransferCreatedEvent(request.From, request.To, request.Amount));


            return await Task.FromResult(true);
        }
    }
}