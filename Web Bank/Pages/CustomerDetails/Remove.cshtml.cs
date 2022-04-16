using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Web_Bank.Data;


namespace Web_Bank.Pages.Customer
{
    public class RemoveModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public RemoveModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Id { get; set; }
        [MaxLength(50)]
        public string Givenname { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(20)]
        public string NationalId { get; set; }
        public string EmailAddress { get; set; }

        

        public IActionResult OnGet(int customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            var customer = _dbContext.Customers.Find(customerId);
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            EmailAddress = customer.EmailAddress;
            NationalId = customer.NationalId;
            Id = customer.Id;

            if (customer == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPostAsync(int customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            var customer = _dbContext.Customers.Find(customerId);

            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
