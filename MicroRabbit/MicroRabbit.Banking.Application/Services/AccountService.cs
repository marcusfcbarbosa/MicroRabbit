using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain._1_Models;
using MicroRabbit.Banking.Domain._2_Interfaces;
using MicroRabbit.Banking.Domain._3_Commands;
using MicroRabbit.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _eventBus;
        public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        //enviando um commando, através do MediatR
        //que irá engatilhar um evento
        //que sera consumido pelo EventHandler     
        public void Transfer(AccountTransferModel accountTrasnfer)
        {
            var createTransferCommand = new CreateTransferCommand(accountTrasnfer.FromAccount,
                accountTrasnfer.ToAccount, 
                accountTrasnfer.TransferAmount);

            _eventBus.SendCommand(createTransferCommand);
        }
    }
}
