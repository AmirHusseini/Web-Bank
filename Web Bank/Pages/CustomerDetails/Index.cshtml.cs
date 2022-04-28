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

        public List<CustomersViewModel> Customers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            Customers = await _dbContext.Customers.Select(e => new CustomersViewModel 
            { 
                Id = e.Id,
                Givenname = e.Givenname,
                Surname = e.Surname,
                NationalId = e.NationalId,
                Streetaddress = e.Streetaddress,
                City = e.City,
                EmailAddress = e.EmailAddress,
                Telephone = e.Telephone
                 
            }).ToListAsync();


            if (Customers == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
