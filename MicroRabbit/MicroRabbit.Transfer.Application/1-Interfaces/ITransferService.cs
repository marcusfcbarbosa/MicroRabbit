using MicroRabbit.Transfer.Domain._6_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Transfer.Application._1_Interfaces
{
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTransferLogs();

    }
}
