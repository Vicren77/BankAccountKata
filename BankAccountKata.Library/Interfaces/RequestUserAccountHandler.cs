using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataGrpc;

namespace BankAccountKata.Library.Interfaces
{
    public class RequestUserAccountHandler : IRequestUserAccountHandler
    {
        private IBankAccountOperationHandler _operationHandler;
        public RequestUserAccountHandler(IBankAccountOperationHandler operationHandler)
        {
            _operationHandler = operationHandler;
        }

        public Task<AccountEntity> RequestCreateAccount(AccountEntity accountEntity)
        {
            return _operationHandler.CreateAccount(accountEntity);
        }

        public Task<AccountEntity> MakeDepositRequest(AccountEntity accountEntity)
        {
            return _operationHandler.Deposit(accountEntity);
        }
    }
}
