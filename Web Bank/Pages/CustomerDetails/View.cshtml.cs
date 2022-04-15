using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Web_Bank.Data;


namespace Web_Bank.Pages.Customer
{
    public class ViewModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ViewModel(ApplicationDbContext dbContext)
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

        

        public IActionResult OnGetAsync(int customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            var customer = _dbContext.Customers.Find(customerId);
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress;
            City = customer.City;
            Country = customer.Country;
            CountryCode = customer.CountryCode;
            Telephone = customer.Telephone;
            TelephoneCountryCode = customer.TelephoneCountryCode;
            EmailAddress = customer.EmailAddress;
            Birthday = customer.Birthday;
            NationalId = customer.NationalId;
            Zipcode = customer.Zipcode;
            Id = customer.Id;

            if (customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
