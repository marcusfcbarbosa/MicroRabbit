﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Banking.Application.Models
{
    public class AccountTransferModel
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
