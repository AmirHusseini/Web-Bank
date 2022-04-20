using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Bank.Data;

namespace Web_Bank.Pages.CustomerAccounts
{
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        
        public WithdrawModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public List<SelectListItem> Accounts { get; set; }
        public List<Data.Customer> customers { get; set; }
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
            customers = _dbContext.Customers.ToList();
        }
        public IActionResult OnGetFetchInfo(int id)
        {
            var account = _dbContext.Accounts.First(e => e.Id == id);
            return new JsonResult(new
            {
                balance = account.Balance
                
            });

        }
        public IActionResult OnPostUpdate()
        {
            return Page();
        }

    }
}
