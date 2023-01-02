using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
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

namespace BankAccountKataGrpc.Host.UnitTest
{
   
        [TestClass()]
        public class AccountRepositoryTests : IDbContextFactory<BankAccountDbContext>
        {
            private DbContextOptions<BankAccountDbContext> _options;


            public AccountRepositoryTests(string dbName = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=BankAccount;Integrated Security=True;Encrypt=False")
            {
                _options = new DbContextOptionsBuilder<BankAccountDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            }

            [TestInitialize()]
            public void Initalize()
            {
                _options = new DbContextOptions<BankAccountDbContext>();
            }
            [TestMethod()]
            public void WithdrawTest()
            {
                // Arrange
                double balance = 10.0;
                double withdrawal = 5.0;
                double expected = 5.0;
                var entity = new AccountEntity { Name = "Trump", Amount = balance };
                var account = new AccountRepository(new AccountRepositoryTests());
                // Act

                account.CreateAccount(entity);
                var a = account.Withdraw(new AccountEntity { Name = "Trump", Amount = withdrawal });
                // Assert
                Assert.AreEqual(expected, a.Result.Amount);


            }


            public BankAccountDbContext CreateDbContext()
            {
                return new BankAccountDbContext(_options);
            }
        }
}
