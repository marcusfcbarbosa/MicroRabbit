using MicroRabbit.Transfer.Data._1_Context;
using MicroRabbit.Transfer.Domain._5_Interfaces;
using MicroRabbit.Transfer.Domain._6_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Transfer.Data._2_Repository
{
    public class TransferRepository : ITransferRepository
    {
        private TransferDbContext _context;
        public TransferRepository(TransferDbContext context)
        {
            _context = context;
        }

        public void Add(TransferLog transferLog)
        {
            _context.TransferLogs.Add(transferLog);
            _context.SaveChanges();
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _context.TransferLogs;
        }
    }
}
