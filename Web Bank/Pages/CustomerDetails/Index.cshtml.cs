using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            Customers = _dbContext.Customers.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Givenname = c.Givenname,
                Surname = c.Surname,
                Birthday = c.Birthday,
                EmailAddress = c.EmailAddress,
                Telephone = c.Telephone
            }
            ).ToList();
        }
    }
}
