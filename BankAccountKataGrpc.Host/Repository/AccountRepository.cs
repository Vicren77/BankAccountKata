using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountKataGrpc.Host.BankAccountDb;
using BankAccountKata.Library;
using BankAccountKata.Library.Interfaces;
using Microsoft.EntityFrameworkCore;
using BankAccountKataGrpc;
using System.Globalization;

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

                record.Balance = record.Balance + accountEntity.Amount;
                dbContext.SaveChanges();
                return Task.FromResult(new AccountEntity
                {
                    Name = accountEntity.Name,
                    Amount = dbContext.Accounts.Where(name => name.Name == accountEntity.Name).Select(balance => balance.Balance).FirstOrDefault(),
                });


            }

        }
    }
}
