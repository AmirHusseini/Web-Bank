#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Bank.Data;

namespace Web_Bank.Pages.Customers
{   [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
        public string Telephone { get; set; }
        [MaxLength(50)]
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
            Streetaddress = customer.Streetaddress;
            City = customer.City;
            Country = customer.Country;
            Telephone = customer.Telephone;
            EmailAddress = customer.EmailAddress;
            Zipcode = customer.Zipcode;

            if (customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        
        public IActionResult OnPost(int customerId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }           

            try
            {
                var customer = _dbContext.Customers.Find(customerId);
                customer.Givenname = Givenname;
                customer.Surname = Surname;
                customer.Streetaddress = Streetaddress;
                customer.City = City;
                customer.Country = Country;
                customer.Telephone = Telephone;
                customer.EmailAddress = EmailAddress;
                customer.Zipcode = Zipcode;
                _dbContext.Customers.Update(customer);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
        public bool CustomerExists(int customerId)
        {
            return _dbContext.Customers.Any(e => e.Id == customerId);
        }

    }
}
