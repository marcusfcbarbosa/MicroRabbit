using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Banking.Domain._1_Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
