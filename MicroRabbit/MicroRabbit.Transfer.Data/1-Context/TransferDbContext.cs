using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Transfer.Data._1_Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions options)
               : base(options){}

    }
}
