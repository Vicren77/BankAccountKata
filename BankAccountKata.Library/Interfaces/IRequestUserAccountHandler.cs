using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataGrpc;

namespace BankAccountKata.Library.Interfaces
{
    public interface IRequestUserAccountHandler
    {
        public Task<AccountEntity> RequestCreateAccount(AccountEntity accountEntity);
        public Task<AccountEntity> MakeDepositRequest(AccountEntity accountEntity);
    }
}
