using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataGrpc;
using Grpc.Core;

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

        public Task<AccountEntity> MakeWithdrawRequest(AccountEntity accountEntity)
        {
            return _operationHandler.Withdraw(accountEntity);
        }

        public async Task GetHistory(HistoryRequest historyRequest, IServerStreamWriter<HistoryReply> responseStream)
        {
            await _operationHandler.GetTransaction(historyRequest, responseStream);
        }
    }
}
