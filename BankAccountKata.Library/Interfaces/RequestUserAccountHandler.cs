using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountKata.Library.Interfaces
{
    public class RequestUserAccountHandler : IRequestUserAccountHandler
    {
        private IBankAccountOperationHandler _operationHandler;
        public RequestUserAccountHandler(IBankAccountOperationHandler operationHandler)
        {
            _operationHandler = operationHandler;
        }

        public void RequestCreateAccount(AccountEntity accountEntity)
        {
            _operationHandler.CreateAccount(accountEntity);
        }

        public double RequestDeposit(AccountEntity accountEntity)
        {
            return _operationHandler.Deposit(accountEntity);
        }
    }
}
