using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountKata.Library.Interfaces
{
    public interface IBankAccountOperationHandler
    {
        public void CreateAccount(AccountEntity accountEntity);
        public double Deposit(AccountEntity accountEntity);

    }
}
