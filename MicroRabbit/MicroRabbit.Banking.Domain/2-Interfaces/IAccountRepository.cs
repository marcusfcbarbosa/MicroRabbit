using MicroRabbit.Banking.Domain._1_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Banking.Domain._2_Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();

    }
}