using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain._1_Models;
using MicroRabbit.Banking.Domain._2_Interfaces;
using System.Collections.Generic;

namespace MicroRabbit.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _context;
        public AccountRepository(BankingDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts;
        }
    }
}