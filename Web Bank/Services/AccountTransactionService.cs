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

        public List<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.Include(a => a.Accounts).ThenInclude(a => a.Transactions).ToList();
        }

        public List<Account> GetAccounts(int id)
        {
            return _dbContext.Accounts.Where(a => a.Id == id).ToList();
        }

        public void Update(Account account)
        {
            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }
    }
}
