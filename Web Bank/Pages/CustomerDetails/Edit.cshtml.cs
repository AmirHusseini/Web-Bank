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
using Web_Bank.ViewModels;

namespace Web_Bank.Pages.Customers
{   
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Display(Name = "Given Name")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Givenname { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Surname { get; set; }

        [Display(Name = "Street Address")]
        [Required]
        [MaxLength(50)]
        public string Streetaddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        [Required]
        [MaxLength(10)]
        public string Zipcode { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [DataType(DataType.EmailAddress)]

        public string EmailAddress { get; set; }
        public async Task<IActionResult> OnGetAsync(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }
            var currentcustomer = await _dbContext.Customers.FirstAsync(c => c.Id == customerId);

            Givenname = currentcustomer.Givenname;
            Surname = currentcustomer.Surname;
            Streetaddress = currentcustomer.Streetaddress;
            City = currentcustomer.City;
            Country = currentcustomer.Country;
            Telephone = currentcustomer.Telephone;
            EmailAddress = currentcustomer.EmailAddress;
            Zipcode = currentcustomer.Zipcode;                                                                               

            if (currentcustomer == null)
            {
                return NotFound();
            }
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync(int customerId)
        {
            if (ModelState.IsValid)
            {
                var currentcustomer = await _dbContext.Customers.FirstAsync(a => a.Id == customerId);

                if (currentcustomer == null)
                {
                    return NotFound();
                }
                
                currentcustomer.Givenname = Givenname;
                currentcustomer.Surname = Surname;
                currentcustomer.Streetaddress = Streetaddress;
                currentcustomer.City = City;
                currentcustomer.Country = Country;
                currentcustomer.Telephone = Telephone;
                currentcustomer.EmailAddress = EmailAddress;
                currentcustomer.Zipcode = Zipcode;               

                _dbContext.Customers.Update(currentcustomer);
                await _dbContext.SaveChangesAsync();

                return RedirectToPage("./Index");
            }           

            //try
            //{
                
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CustomerExists(customerId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            return Page();
            
        }
        //public bool CustomerExists(int customerId)
        //{
        //    return _dbContext.Customers.Any(e => e.Id == customerId);
        //}

    }
}
