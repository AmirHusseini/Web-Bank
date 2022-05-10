using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Bank.Data;
using Web_Bank.Services;

namespace UnitTest
{
    [TestClass]
    public class AccountTransactionTest
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AccountTransactionService _transactionService;

        public AccountTransactionTest()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb").Options;

            _dbContext = new ApplicationDbContext(option);
            _transactionService = new AccountTransactionService(_dbContext);

        }

        // Withdraw

        [TestMethod]
        [DataRow(100)]
        public async Task Withdraw_IfEverythingCorrect(int amount)
        {
            //Arrange
            var account =  new Account() 
            { 
                Id = 1, 
                AccountType = "Checking", 
                Balance = 150, 
                Created = DateTime.Now, 
                Transactions = new() 
            };            
            
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            
            decimal expectedbalance = account.Balance - amount;
            
            //Act
            await _transactionService.WithdrawAsync(account.Id, amount);

            //Assert
            Assert.AreEqual(expectedbalance, account.Balance);

        }

        [TestMethod]
        [DataRow(0)]
        public async Task CanNotWithdraw_IfAmountIsZero(int amount)
        {
            //Arrange
            var account = new Account()
            {
                Id = 2,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            //Act
            var result = await Assert.ThrowsExceptionAsync<ArgumentException>(() =>  _transactionService.WithdrawAsync(account.Id, amount));

            //Assert
            Assert.AreEqual("Amount can not be null or negative.", result.Message);

        }
        [TestMethod]
        [DataRow(-1)]
        public async Task CanNotWithdraw_IfAmountIsNegative(int amount)
        {
            //Arrange
            var account = new Account()
            {
                Id = 3,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            //Act
            var result = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _transactionService.WithdrawAsync(account.Id, amount));

            //Assert
            Assert.AreEqual("Amount can not be null or negative.", result.Message);

        }

        [TestMethod]
        [DataRow(200)]
        public async Task CanNotWithdraw_IfAmountIsMoreThanBalance(int amount)
        {
            //Arrange
            var account = new Account()
            {
                Id = 4,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();            

            //Act
            var result = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _transactionService.WithdrawAsync(account.Id, amount));

            //Assert
            Assert.AreEqual("Amount is more than balance.", result.Message);

        }

        // Deposit 

        [TestMethod]
        [DataRow(100)]
        public async Task Deposit_IfEverythingCorrect(int amount)
        {
            //Arrange
            var account = new Account()
            {
                Id = 5,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            string operation = "Transfer";
            decimal expectedbalance = account.Balance + amount;

            //Act
            await _transactionService.DepositAsync(operation, account.Id, amount);

            //Assert
            Assert.AreEqual(expectedbalance, account.Balance);

        }
        [TestMethod]
        [DataRow(0)]
        public async Task CanNotDeposit_IfAmountIsZero(int amount)
        {
            //Arrange
            var account = new Account()
            {
                Id = 6,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            string operation = "Deposit";

            //Act
            var result = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _transactionService.DepositAsync(operation, account.Id, amount));

            //Assert
            Assert.AreEqual("Amount can not be null or negative.", result.Message);

        }
        [TestMethod]
        [DataRow(-1)]
        public async Task CanNotDeposit_IfAmountIsNegative(int amount)
        {
            //Arrange
            var account = new Account()
            {
                Id = 7,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            string operation = "Deposit";

            //Act
            var result = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _transactionService.DepositAsync(operation, account.Id, amount));

            //Assert
            Assert.AreEqual("Amount can not be null or negative.", result.Message);

        }

        [TestMethod]
        [DataRow(null, 200)]
        public async Task CanNotDeposit_IfOperationIsNull(string operation, int amount)
        {
            //Arrange
            var account = new Account()
            {
                Id = 14,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            //Act
            var result = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _transactionService.DepositAsync(operation, account.Id, amount));

            //Assert
            Assert.AreEqual("Value cannot be null.", result.Message);

        }

        // Transfer

        [TestMethod]
        [DataRow(100)]
        public async Task Transfer_IfEverythingCorrect(int amount)
        {
            //Arrange
            var accountfrom = new Account()
            {
                Id = 8,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            var accountto = new Account()
            {
                Id = 9,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };
            _dbContext.Accounts.AddRange(accountfrom, accountto);
            _dbContext.SaveChanges();

            decimal expectedbalance = accountfrom.Balance - amount;

            //Act
            await _transactionService.TransferAsync(accountfrom.Id, accountto.Id, amount);

            //Assert
            Assert.AreEqual(expectedbalance, accountfrom.Balance);

        }
        [TestMethod]
        [DataRow(0)]
        public async Task CanNotTransfer_IfAmountIsZero(int amount)
        {
            //Arrange
            var accountfrom = new Account()
            {
                Id = 10,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            var accountto = new Account()
            {
                Id = 11,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };
            _dbContext.Accounts.AddRange(accountfrom, accountto);

            _dbContext.SaveChanges();

            //Act
            var result = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _transactionService.TransferAsync(accountfrom.Id, accountto.Id, amount));

            //Assert
            Assert.AreEqual("Amount can not be null or negative.", result.Message);

        }
        [TestMethod]
        [DataRow(-1)]
        public async Task CanNotTransfer_IfAmountIsNegative(int amount)
        {
            //Arrange
            var accountfrom = new Account()
            {
                Id = 12,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };

            var accountto = new Account()
            {
                Id = 13,
                AccountType = "Checking",
                Balance = 150,
                Created = DateTime.Now,
                Transactions = new()
            };
            _dbContext.Accounts.AddRange(accountfrom, accountto);
            _dbContext.SaveChanges();


            //Act
            var result = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _transactionService.TransferAsync(accountfrom.Id, accountto.Id, amount));

            //Assert
            Assert.AreEqual("Amount can not be null or negative.", result.Message);

        }

    }
}
