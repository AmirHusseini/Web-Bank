using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Bank.Data;
using Web_Bank.Services;

namespace Web_Bank.Pages.CustomerAccounts
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAccountTransactionService _transactionService;
        public WithdrawModel(ApplicationDbContext dbContext, IAccountTransactionService transactionService)
        {
            _dbContext = dbContext;
            _transactionService = transactionService;
        }
        
        public Decimal Amount { get; set; }
        public int AccountId { get; set; }
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
                balance = account.Balance,
                currentaccountid = account.Id
            });

        }
        public IActionResult OnPostUpdate(int accountId, int amount)
        {
            if (ModelState.IsValid)
            {
                if (_transactionService.CanWithdraw(accountId, amount))
                {
                    _transactionService.Withdraw(accountId, amount);
                    return RedirectToPage("./Transactions", new { accountId = accountId });
                }
                else
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                }               
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid attempt.");
                return Page();
            }
            return Page();


        }

    }
}
