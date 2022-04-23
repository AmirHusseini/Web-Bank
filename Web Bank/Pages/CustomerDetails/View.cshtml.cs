using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Web_Bank.Data;
using Web_Bank.ViewModels;

namespace Web_Bank.Pages.Customer
{
    public class ViewModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public InputViewModel customer { get; set; }



        public async Task<IActionResult> OnGetAsync(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            customer = await _dbContext.Customers.Select(a => new InputViewModel
            {
                Id = a.Id,
                Givenname = a.Givenname,
                Surname = a.Surname,
                Streetaddress = a.Streetaddress,
                City = a.City,
                Country = a.Country,
                Telephone = a.Telephone,
                EmailAddress = a.EmailAddress,
                Birthday = a.Birthday,
                NationalId = a.NationalId,
                Zipcode = a.Zipcode
            }).FirstAsync(c => c.Id == customerId);            

            if (customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
