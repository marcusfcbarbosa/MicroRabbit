using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Domain._3_Events;
using MicroRabbit.Transfer.Domain._5_Interfaces;
using MicroRabbit.Transfer.Domain._6_Models;
using System.Threading.Tasks;

namespace MicroRabbit.Transfer.Domain._4_EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;
        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            _transferRepository.Add(new TransferLog()
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                TransferAmount = @event.Amount
            });
            return Task.CompletedTask;
        }
    }
}