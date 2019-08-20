using MicroRabbit.Transfer.Domain._6_Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Transfer.Data._1_Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions options)
               : base(options){}
        public DbSet<TransferLog> TransferLogs { get; set; }
    }
}