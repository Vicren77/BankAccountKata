using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountKata.Library.Interfaces
{
    public interface IRequestUserAccountHandler
    {
        public void RequestCreateAccount(AccountEntity accountEntity);
        public double RequestDeposit(AccountEntity accountEntity);
    }
}
