using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Banking.Domain._3_Commands
{
    public class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(int from, int to, decimal amount)
        {
            this.From = from;
            this.To = to;
            this.Amount = amount;
        }
    }
}
