using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web_Bank.Data;
using Web_Bank.ViewModels;

namespace Web_Bank.Pages.CustomerAccounts
{
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionsModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<TransactionsViewModel> AllTransactions { get; set; }

        public void OnGet(int customerId, int accountId)
        {
            var customer = _dbContext.Customers
                .Include(a => a.Accounts)
                .ThenInclude(a => a.Transactions)
                .FirstOrDefault(c => c.Id == customerId);
            
            AllTransactions = customer.Accounts
                .FirstOrDefault(a => a.Id == accountId).Transactions
                
                .Select(t => new TransactionsViewModel
                {
                    Id = t.Id,
                    Type = t.Type,
                    Operation = t.Operation,
                    Date = t.Date,
                    Amount = t.Amount,
                    NewBalance = t.NewBalance                    
                }).OrderByDescending(a => a.Date).ToList();
            
        }
    }
}
