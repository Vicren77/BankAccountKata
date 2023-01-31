using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataWebAPI.BankAccountDb;
using BankAccountKata.Library;
using BankAccountKata.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using BankAccountKata;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Grpc.Core;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BankAccountKataGrpc.Host.Repository
{
    public class AccountRepository : IBankAccountOperationHandler
    {
        private readonly IDbContextFactory<BankAccountDbContext> _dbContextFactory;

        public AccountRepository(IDbContextFactory<BankAccountDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public Task<AccountEntity> CreateAccount(AccountEntity accountEntity)
        {
            if(accountEntity.Name == "") { throw new ArgumentException(nameof(accountEntity.Name), "account creation name input shoud not be void");}
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                if (dbContext.Accounts.Any(name => name.Name == accountEntity.Name))
                {
                    return Task.FromResult(new AccountEntity
                    {
                        Name = accountEntity.Name + " Account Exists",
                        Amount = dbContext.Accounts.Where(name => name.Name == accountEntity.Name).Select(balance => balance.Balance).FirstOrDefault()
                    }); ;
                }
                Accounts accounts = new Accounts()
                {
                    Name = accountEntity.Name,
                    Balance = accountEntity.Amount,
                };
                dbContext.Accounts.AddRange(accounts);
                dbContext.SaveChanges();
                return Task.FromResult(new AccountEntity
                {
                    Name = accountEntity.Name,
                    Amount = accountEntity.Amount,
                });
            }
        }

        public Task<AccountEntity> Deposit(AccountEntity accountEntity)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var record = dbContext.Accounts.FirstOrDefault(account => account.Name == accountEntity.Name);
                if (record == null) { throw new ArgumentException(nameof(accountEntity.Name), "you are trying to add money from non existing account"); }

                record.Balance = record.Balance + accountEntity.Amount;

                History history = new History
                {
                    Name = accountEntity.Name,
                    Operation = "Deposit",
                    Date = DateTime.Now,
                    Amount = accountEntity.Amount,
                    Balance = record.Balance,
                };
                dbContext.History.AddRange(history);
                dbContext.SaveChanges();

                return Task.FromResult(new AccountEntity
                {
                    Name = accountEntity.Name,
                    Amount = dbContext.Accounts.Where(name => name.Name == accountEntity.Name).Select(balance => balance.Balance).FirstOrDefault(),
                });


            }

        }
        public Task<AccountEntity> Withdraw(AccountEntity accountEntity)
        {

            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var record = dbContext.Accounts.FirstOrDefault(account => account.Name == accountEntity.Name);
                if(record == null) { throw new ArgumentException(nameof(accountEntity.Name), "you are trying to withdray money from non existing account"); }
                if(record.Balance <= accountEntity.Amount) { throw new ArgumentException(nameof(accountEntity.Amount), "Withdrawal exceeds balance!"); }
                record.Balance = record.Balance - accountEntity.Amount;

                History history = new History
                {
                    Name=accountEntity.Name,
                    Operation = "Withdraw",
                    Date = DateTime.Now,
                    Amount = accountEntity.Amount,
                    Balance = record.Balance,
                };
                dbContext.History.AddRange(history);
                dbContext.SaveChanges();

                return Task.FromResult(new AccountEntity
                {
                    Name = accountEntity.Name,
                    Amount = dbContext.Accounts.Where(name => name.Name == accountEntity.Name).Select(balance => balance.Balance).FirstOrDefault(),
                });

            }
        }
        public async Task GetTransaction(HistoryRequest historyRequest, IServerStreamWriter<HistoryReply> responseStream)
        {

            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var checkRecord = dbContext.History.FirstOrDefault(account => account.Name == historyRequest.Name);
                if (checkRecord == null) { throw new ArgumentException(nameof(historyRequest.Name), "you are trying to get history transaction from non existing account"); }

                var record = dbContext.History.Where(account => account.Name == historyRequest.Name).ToList();
                foreach(var transaction in record)
                {
                    await responseStream.WriteAsync(new HistoryReply
                    {
                        Name = transaction.Name,
                        OperationType = transaction.Operation,
                        OperationDate = transaction.Date.ToString(),
                        Amount = transaction.Amount,
                        Balance = transaction.Balance
                    });
                }
                 
            }

        }
        
    }
}
