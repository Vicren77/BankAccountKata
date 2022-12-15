using BankAccountKata.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountKata.Library
{
    public class BankAccountService 
    {
        private readonly IRequestUserAccountHandler _requestUserAccountHandler;
        public BankAccountService(IRequestUserAccountHandler requestUserAccountHandler)
        {
            _requestUserAccountHandler = requestUserAccountHandler;
        }
        public void RequestCreateAccountService(AccountEntity accountEntity)
        {
            _requestUserAccountHandler.RequestCreateAccount(accountEntity);   
        }
        public double RequestDepositService(AccountEntity accountEntity)
        {
            return _requestUserAccountHandler.RequestDeposit(accountEntity);
        }
    }
}
