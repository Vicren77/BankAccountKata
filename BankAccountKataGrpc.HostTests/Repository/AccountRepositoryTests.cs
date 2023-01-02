using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountKataGrpc.Host.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankAccountKataGrpc.Host.BankAccountDb;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Grpc.Core;

namespace BankAccountKataGrpc.Host.Repository.Tests
{
    [TestClass()]
    public class AccountRepositoryTests : IDbContextFactory<BankAccountDbContext>
    {
        private DbContextOptions<BankAccountDbContext> _options;
        public AccountRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<BankAccountDbContext>()
            .UseInMemoryDatabase("Data Source=MSI\\SQLEXPRESS;Initial Catalog=BankAccount;Integrated Security=True;Encrypt=False")
            .Options;
        }

        [TestMethod()]
        public void CreateAccountTest()
        {
            double amount = 10;
            var entity = new AccountEntity { Name = "Trump", Amount = amount };
            var account = new AccountRepository(new AccountRepositoryTests());
            
            //act
            var result = account.CreateAccount(entity);
            //assert
            Assert.AreEqual(result.Result, entity);

        }

        [TestMethod()]
        public void WithdrawTest()
        {
            // Arrange
            double withdrawal = 5.0;
            double expected = 5.0;
            var account = new AccountRepository(new AccountRepositoryTests());
            // Act
           
            var a =  account.Withdraw(new AccountEntity { Name = "Trump", Amount = withdrawal });
            // Assert
            
            Assert.AreEqual(expected, a.Result.Amount);
           
        }
        [TestMethod()]
        public void DepositTest()
        {
            // Arrange
            double deposit = 10.0;
            double expected = 15.0;
            var account = new AccountRepository(new AccountRepositoryTests());
            // Act

            var a = account.Deposit(new AccountEntity { Name = "Trump", Amount = deposit });
            // Assert
            Assert.AreEqual(expected, a.Result.Amount);
        }

        [TestMethod()]
        public void GetTransactionTest()
        {
            var account = new AccountRepository(new AccountRepositoryTests());

            //account.GetTransaction(new HistoryRequest() { Name = "Trump" });
        }

        public BankAccountDbContext CreateDbContext()
        {
            return new BankAccountDbContext(_options);
        }
    }
}