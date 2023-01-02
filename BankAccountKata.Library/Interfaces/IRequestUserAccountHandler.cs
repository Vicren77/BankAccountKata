using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataGrpc;
using Grpc.Core;

namespace BankAccountKata.Library.Interfaces
{
    public interface IRequestUserAccountHandler
    {
        public Task<AccountEntity> RequestCreateAccount(AccountEntity accountEntity);
        public Task<AccountEntity> MakeDepositRequest(AccountEntity accountEntity);

        public Task<AccountEntity> MakeWithdrawRequest(AccountEntity accountEntity);
        public Task GetHistory(HistoryRequest historyRequest, IServerStreamWriter<HistoryReply> responseStream);
    }
}
