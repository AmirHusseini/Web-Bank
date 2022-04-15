using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_Bank.Data;
using Web_Bank.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Web_Bank.Pages.Customer
{   [BindProperties]
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public NewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Id { get; set; }
        [MaxLength(50)]
        public string Givenname { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string Streetaddress { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(10)]
        public string Zipcode { get; set; }
        [MaxLength(30)]
        public string Country { get; set; }
        [MaxLength(2)]
        public string CountryCode { get; set; }
        [MaxLength(20)]
        public string NationalId { get; set; }
        [Range(0, 9999)]
        public int TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }       

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var customer = new Data.Customer();

            customer.Givenname = Givenname;
            customer.Surname = Surname;
            customer.Streetaddress = Streetaddress;
            customer.City = City;
            customer.Country = Country;
            customer.CountryCode = CountryCode;
            customer.Telephone = Telephone;
            customer.TelephoneCountryCode = TelephoneCountryCode;
            customer.EmailAddress = EmailAddress;
            customer.Birthday = Birthday;
            customer.Id = Id;
            customer.NationalId = NationalId;
            customer.Zipcode = Zipcode;

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
