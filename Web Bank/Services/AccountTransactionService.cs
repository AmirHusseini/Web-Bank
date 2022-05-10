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

        public List<Account> GetAllAccounts(int? customerId)
        {
            return _dbContext.Customers
                .Include(a => a.Accounts)
                .FirstOrDefault(c => c.Id == customerId).Accounts.ToList();
        }

        public Account GetAccount(int accountId)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);
        }
       

        public async Task WithdrawAsync(int accountId, int amount)
        {
            var account = await _dbContext.Accounts.Include(t => t.Transactions).FirstOrDefaultAsync(a => a.Id == accountId);
            
            if (account == null)
            {
                throw new ArgumentNullException();
            }

            else if (amount <= 0)
            {

                throw new ArgumentException("Amount can not be null or negative.");

            }

            else if (amount > account.Balance)
            {
                throw new ArgumentException("Amount is more than balance.");
            }

            else
            {
                account.Balance -= amount;

                var transaction = new Transaction
                {
                    Amount = amount,
                    Date = DateTime.Now,
                    NewBalance = account.Balance,
                    Operation = "ATM withdrawal",
                    Type = "Credit" //kam shode faghat esme operation awaz mishe
                };
                            
                 account.Transactions.Add(transaction);
                 await _dbContext.SaveChangesAsync();
            }      
            
        }                

        public async Task DepositAsync(string operation, int accountId, int amount)
        {
            var account = await _dbContext.Accounts.Include(t => t.Transactions).FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                throw new ArgumentNullException();
            }

            else if (amount <= 0)
            {

                throw new ArgumentException("Amount can not be null or negative.");

            }
            else if (operation == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                account.Balance += amount;

                var transaction = new Transaction
                {
                    Amount = amount,
                    Date = DateTime.Now,
                    NewBalance = account.Balance,
                    Operation = operation.ToString(),
                    Type = "Debit"
                };

                account.Transactions.Add(transaction);
                await _dbContext.SaveChangesAsync();
            }            
        }

        public async Task TransferAsync(int accountIdfrom, int accountIdto, int amount)
        {
            var accountfrom = await _dbContext.Accounts.Include(t => t.Transactions).FirstOrDefaultAsync(a => a.Id == accountIdfrom);
            var accountto = await _dbContext.Accounts.Include(t => t.Transactions).FirstOrDefaultAsync(a => a.Id == accountIdto);

            if (accountfrom == null || accountto == null)
            {
                throw new NotImplementedException();
            }
            else if (amount <= 0)
            {

                throw new ArgumentException("Amount can not be null or negative.");

            }
            else if (amount > accountfrom.Balance)
            {
                throw new ArgumentException("Amount is more than balance.");
            }

            else
            {
                accountto.Balance += amount;

                var transaction = new Transaction
                {
                    Amount = amount,
                    Date = DateTime.Now,
                    NewBalance = accountto.Balance,
                    Operation = "Transfer",
                    Type = "Debit"
                };

                accountfrom.Balance -= amount;

                var transaction2 = new Transaction
                {
                    Amount = amount,
                    Date = DateTime.Now,
                    NewBalance = accountfrom.Balance,
                    Operation = "Transfer",
                    Type = "Credit" //kam shode faghat esme operation awaz mishe
                };
                accountto.Transactions.Add(transaction);
                accountfrom.Transactions.Add(transaction2);
                await _dbContext.SaveChangesAsync();
            }
        }
       
    }
}
