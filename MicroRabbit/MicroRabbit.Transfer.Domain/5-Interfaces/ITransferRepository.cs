using MicroRabbit.Transfer.Domain._6_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Transfer.Domain._5_Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
    }
}
