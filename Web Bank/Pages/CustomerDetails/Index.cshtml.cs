using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_Bank.Data;
using Web_Bank.ViewModels;

namespace Web_Bank.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CustomerViewModel> Customers { get; set; }
        

        public void OnGet()
        {
            Customers = (from c in _dbContext.Customers
                         join a in _dbContext.Accounts on c.Id equals a.Id
                         where c.Id == a.Id
                         select new CustomerViewModel
                         {
                             Id = c.Id,
                             Givenname = c.Givenname,
                             Surname = c.Surname,
                             EmailAddress = c.EmailAddress,
                             Birthday = c.Birthday,
                             Telephone = c.Telephone,
                             Balance = a.Balance
                         }).ToList();

            //Customers = _dbContext.Customers.Include(a => a.Accounts)
            //.Select(c => new CustomerViewModel
            //{
            //    Id = c.Id,
            //    Givenname = c.Givenname,
            //    Surname = c.Surname,
            //    Birthday = c.Birthday,
            //    EmailAddress = c.EmailAddress,
            //    Telephone = c.Telephone,
            //    Balance = c.Accounts.Select(a => a.Balance)

            //}
            //).ToList();
        }
    }
}
