using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Web_Bank.Data;
using Web_Bank.Services;

namespace Web_Bank.Pages.CustomerAccounts
{
    public class IndexModel : PageModel
    {
        //private readonly AccountTransactionService _account;
        private readonly ApplicationDbContext _dbContext;

        public IndexModel( ApplicationDbContext context)
        {
           
            _dbContext = context;
        }
        public List<Account>  Accounts{ get; set; }
        public Data.Customer Customer { get; set; }

        public void OnGet(int customerId)
        {
            if (customerId == null)
            {
                
            }
            Customer = _dbContext.Customers.Include(a => a.Accounts).FirstOrDefault(c => c.Id == customerId);
            //Accounts = _dbContext.Accounts.Select(a => a. == customerId).ToList();
        }
    }
}
