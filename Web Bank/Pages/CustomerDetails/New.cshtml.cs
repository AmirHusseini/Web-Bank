using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_Bank.Data;
using Web_Bank.ViewModels;


namespace Web_Bank.Pages.Customer
{   
    
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public NewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [BindProperty]
        public NewCustomerViewModel Input { get; set; }        

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var newcustomer = new Data.Customer()
                {
                    Givenname = Input.Givenname,
                    Surname = Input.Surname,
                    EmailAddress = Input.EmailAddress,
                    Birthday = Input.Birthday,
                    City = Input.City,
                    Country = Input.Country,
                    CountryCode = Input.CountryCode,
                    NationalId = Input.NationalId,
                    Telephone = Input.Telephone,
                    Streetaddress = Input.Streetaddress,
                    TelephoneCountryCode = Input.TelephoneCountryCode,
                    Zipcode = Input.Zipcode,
                };
                _dbContext.Customers.Add(newcustomer);

                await _dbContext.SaveChangesAsync();
                return RedirectToPage("./Index");
                
            }

            return Page();

        }
    }
}
