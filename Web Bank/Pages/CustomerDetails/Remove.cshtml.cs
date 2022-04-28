using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Web_Bank.Data;
using Web_Bank.ViewModels;
using Web_Bank.ViewModels.Customer;

namespace Web_Bank.Pages.Customer
{
    public class RemoveModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public RemoveModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public RemoveCustomerViewModel customer { get; set; }        

        public async Task<IActionResult> OnGetAsync(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            customer = await _dbContext.Customers.Select (a => new RemoveCustomerViewModel
            {
                Givenname = a.Givenname,
                Surname = a.Surname,
                EmailAddress = a.EmailAddress,
                NationalId = a.NationalId,
                Id = a.Id,
                Telephone = a.Telephone,
                  

            }).FirstAsync(c => c.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            var currentcustomer = await _dbContext.Customers.FindAsync(customerId);

            if (currentcustomer != null)
            {
                _dbContext.Customers.Remove(currentcustomer);
                await _dbContext.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
