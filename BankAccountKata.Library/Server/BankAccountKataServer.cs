using BankAccountKata.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataGrpc;
using Grpc.Core;
using Microsoft.Extensions.Configuration;

namespace BankAccountKata.Library.Server
{
    public class BankAccountKataServer : BankAccountKataGrpc.BankAccountKata.BankAccountKataBase
    {
        private readonly IRequestUserAccountHandler _requestUserAccountHandler;
        private readonly IConfiguration _configuration;
        public BankAccountKataServer(IConfiguration configuration, IRequestUserAccountHandler requestUserAccountHandler)
        {
            _configuration = configuration;
            _requestUserAccountHandler = requestUserAccountHandler;
        }
        public override Task<AccountEntity> RequestCreateAccount(AccountEntity accountEntityRequest, ServerCallContext context)
        {
            return _requestUserAccountHandler.RequestCreateAccount(accountEntityRequest);
        }
        public override Task<AccountEntity> MakeDepositRequest(AccountEntity accountEntity, ServerCallContext context)
        {
            return _requestUserAccountHandler.MakeDepositRequest(accountEntity);
        }
    }
}
