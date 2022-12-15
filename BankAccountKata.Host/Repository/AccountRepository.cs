using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using BankAccountKata.Host.BankAccountDb;
using BankAccountKata.Library;
using BankAccountKata.Library.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAccountKata.Host.Repository
{
    public class AccountRepository : BankAccountOperationHandler
    {
        private readonly IDbContextFactory<BankAccountDbContext> _dbContextFactory;
        
        public AccountRepository(IDbContextFactory<BankAccountDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public override void CreateAccount(AccountEntity accountEntity)
        {
            using(var dbContext = _dbContextFactory.CreateDbContext())
            {
                if (dbContext.Accounts.Any(name => name.Name == accountEntity.name)) return;
                Accounts accounts = new Accounts()
                {
                    Name = accountEntity.name,
                    Balance = accountEntity.balance,
                };
                dbContext.Accounts.AddRange(accounts);
                dbContext.SaveChanges();
            }
        }

        public override double Deposit(AccountEntity accountEntity)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var record =  dbContext.Accounts.FirstOrDefault(account => account.Name == accountEntity.name);
                if (record != null)
                {
                    record.Balance = record.Balance + accountEntity.balance;
                    dbContext.SaveChanges();
                    return record.Balance;
                }
                return record.Balance;
            }
            
        }
    }
}
