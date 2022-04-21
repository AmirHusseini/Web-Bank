using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Bank.Data;
using Web_Bank.Services;

namespace Web_Bank.Pages.CustomerAccounts
{
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAccountTransactionService _transactionService;
        public WithdrawModel(ApplicationDbContext dbContext, IAccountTransactionService transactionService)
        {
            _dbContext = dbContext;
            _transactionService = transactionService;
        }
        [BindProperty]
        public Decimal Amount { get; set; }

        public List<SelectListItem> Accounts { get; set; }
        
        public void OnGet(int customerId)
        {
            var customer = _dbContext.Customers
                .Include(a => a.Accounts)
                .First(a => a.Id == customerId);

            Accounts = customer.Accounts
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.AccountType,
                    
                }).ToList();
        
        }
        public IActionResult OnGetFetchInfo(int id)
        {
            var account = _transactionService.GetAccount(id);

            return new JsonResult(new
            {
                balance = account.Balance
                
            });

        }
        public IActionResult OnPostUpdate(int accountId, int amount)
        {
            if (!ModelState.IsValid)
            {
                return Page();
                
            }
            else
            {
                var account = _transactionService.GetAccount(accountId);

                if (_transactionService.Withdraw(accountId, amount))
                {
                    account.Balance -= amount;
                    _transactionService.Update(account);
                    
                }
                return Page();
            }
                
            
            
        }

    }
}
