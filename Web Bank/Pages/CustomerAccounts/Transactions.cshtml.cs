using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web_Bank.Data;
using Web_Bank.ViewModels;

namespace Web_Bank.Pages.CustomerAccounts
{
    [BindProperties]
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionsModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<TransactionsViewModel> AllTransactions { get; set; }
        public int AccountId { get; set; }

        public int CustomerId { get; set; }

        public void OnGet(int accountId, int customerId)
        {
             CustomerId = customerId;
             AccountId = accountId;
        }
        public JsonResult OnGetFetchInfo(int id)
        {

            AllTransactions = _dbContext.Accounts
                .Include(x => x.Transactions)
                .FirstOrDefault(a => a.Id == id).Transactions

                .Select(t => new TransactionsViewModel
                {
                    Id = t.Id,
                    Type = t.Type,
                    Operation = t.Operation,
                    Date = t.Date,
                    Amount = t.Amount,
                    NewBalance = t.NewBalance
                }).OrderByDescending(a => a.Date).ToList();
            return new JsonResult (AllTransactions);
        }

    }
}
