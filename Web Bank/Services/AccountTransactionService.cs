using Microsoft.EntityFrameworkCore;
using Web_Bank.Data;

namespace Web_Bank.Services
{

    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly ApplicationDbContext _dbContext;       

        public AccountTransactionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Account> GetAllAccounts()
        {
            return _dbContext.Accounts.ToList();
        }

        public Account GetAccount(int accountId)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);
        }
       

        public void Withdraw(int accountId, int amount)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);
                    
            account.Balance -= amount;

            var transaction = new Transaction
            {
                Amount = amount,
                Date = DateTime.Now,
                NewBalance = account.Balance,
                Operation = "ATM withdrawal",
                Type = "Credit"
            };

            _dbContext.Accounts.Update(account);
            _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId).Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }
        public bool CanWithdraw(int accountId, int amount)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);
            
            if (amount <= 0)
            {

                return false;

            }
            else if (account.Balance < amount)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        //public Account Deposit(int accountId, int belopp)
        //{

        //}
        //public Account Transfer(int accountId, int belopp)
        //{

        //}
        public enum ErrorCode
        {
            // credit= pllus , debit = minus

            Ok,
            BalanceTooLow,
            AmountIsNegative,

        }
    }
}
