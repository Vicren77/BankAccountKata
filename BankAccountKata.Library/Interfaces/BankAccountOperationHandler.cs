using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountKata.Library.Interfaces
{
    public abstract class BankAccountOperationHandler : IBankAccountOperationHandler
    {
        public abstract void CreateAccount(AccountEntity accountEntity);

        public abstract double Deposit(AccountEntity accountEntity);
    }
}
