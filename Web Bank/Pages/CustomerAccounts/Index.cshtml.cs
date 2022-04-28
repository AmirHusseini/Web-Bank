using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_Bank.Data;
using Web_Bank.ViewModels;

namespace Web_Bank.Pages.CustomerAccounts
{
    //[Authorize]
    
    public class IndexModel : PageModel
    {
        
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IndexModel(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {

            _dbContext = context;
            _signInManager = signInManager;
        }

        public CustomerAccountViewModel ViewModelAccounts { get; set; }
        public decimal Total { get; set; }

        public async Task<IActionResult> OnGetAsync(int? customerId)
        {
            if (customerId == null && _signInManager.IsSignedIn(User))
            {
                
                var UserEmail = User.FindFirstValue(ClaimTypes.Email);
                var customer = await _dbContext.Customers.Include(a => a.Accounts).FirstOrDefaultAsync(x => x.EmailAddress == UserEmail);

                if (customer != null)
                {
                    
                    ViewModelAccounts = new CustomerAccountViewModel
                    {
                        Id = customer.Id,
                        Givenname = customer.Givenname,
                        Surname = customer.Surname,
                        Accounts = customer.Accounts
                    };
                    Total = customer.Accounts.Sum(a => a.Balance);
                    
                }
                else
                {
                    return NotFound();
                }                                                   
                                

            }
            else if (customerId != null && _signInManager.IsSignedIn(User) || User.IsInRole("Admin"))
            {                
                var customer = await _dbContext.Customers.Include(a => a.Accounts).FirstOrDefaultAsync(x => x.Id == customerId);
                var customeremail = User.FindFirstValue(ClaimTypes.Email);

                if (customer != null)
                {
                    if (User.IsInRole("Admin"))
                    {
                        ViewModelAccounts = new CustomerAccountViewModel
                        {
                            Id = customer.Id,
                            Givenname = customer.Givenname,
                            Surname = customer.Surname,
                            Accounts = customer.Accounts
                        };
                        Total = ((int)customer.Accounts.Sum(a => a.Balance));
                    }
                    else if (customer.EmailAddress == customeremail)
                    {
                        ViewModelAccounts = new CustomerAccountViewModel
                        {
                            Id = customer.Id,
                            Givenname = customer.Givenname,
                            Surname = customer.Surname,
                            Accounts = customer.Accounts
                        };
                        Total = ((int)customer.Accounts.Sum(a => a.Balance));
                    }
                    else
                    {
                        return LocalRedirect("/Identity/Account/AccessDenied");
                    }
                }
                
                                                          
                
            }
            else
            {
                return LocalRedirect("/Identity/Account/AccessDenied");

            }
            return Page();
        }    
    }
}
