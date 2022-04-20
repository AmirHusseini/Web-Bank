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

        [BindProperty(SupportsGet = true)]
        public int CategoryId { get; set; }
        [BindProperty]
        public decimal Total { get; set; }
        public int SubCategoryId { get; set; }
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
        public IActionResult OnGetSubCategories(int CategoryId)
        {
            Total = _dbContext.Accounts.FirstOrDefault(a => a.Id == CategoryId).Balance;
            return Page();
        }

    }
}
