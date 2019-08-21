using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application._1_Interfaces;
using MicroRabbit.Transfer.Domain._5_Interfaces;
using MicroRabbit.Transfer.Domain._6_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Transfer.Application._2_Services
{
    public class TransferService : ITransferService
    {

        private readonly ITransferRepository _transferRepository;
        private readonly IEventBus _eventBus;
        public TransferService(ITransferRepository transferRepository, IEventBus eventBus)
        {
            _transferRepository = transferRepository;
            _eventBus = eventBus;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _transferRepository.GetTransferLogs();
        }
    }
}
