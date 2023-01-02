using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataGrpc;
using Grpc.Core;

namespace BankAccountKata.Library.Interfaces
{
    public interface IBankAccountOperationHandler
    {
        public Task<AccountEntity> CreateAccount(AccountEntity accountEntity);
        public Task<AccountEntity> Deposit(AccountEntity accountEntity);
        public Task<AccountEntity> Withdraw(AccountEntity accountEntity);
        public Task GetTransaction(HistoryRequest historyRequest, IServerStreamWriter<HistoryReply> responseStream);

    }
}
