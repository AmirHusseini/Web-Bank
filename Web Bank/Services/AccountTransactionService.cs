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

        public void Update(Account account)
        {
            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }

        public bool Withdraw(int accountId, int belopp)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);
            
            if (belopp <= 0)
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
