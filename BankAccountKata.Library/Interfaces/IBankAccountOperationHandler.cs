using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataGrpc;

namespace BankAccountKata.Library.Interfaces
{
    public interface IBankAccountOperationHandler
    {
        public Task<AccountEntity> CreateAccount(AccountEntity accountEntity);
        public Task<AccountEntity> Deposit(AccountEntity accountEntity);

    }
}
