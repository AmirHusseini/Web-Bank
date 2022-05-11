using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Web_Bank.Data;
using Web_Bank.Services;

namespace Web_Bank.Pages.CustomerAccounts
{
    [BindProperties]
    public class TransferModel : PageModel
    {
        private readonly IAccountTransactionService _transactionService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public TransferModel(IAccountTransactionService transactionService, SignInManager<IdentityUser> signInManager, ApplicationDbContext dbContext)
        {
            _transactionService = transactionService;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }
        [Range(5, double.MaxValue, ErrorMessage = "The minimum transfer has to be 5$")]
        public Decimal Amount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You have to choose an option")]
        public int AccountIdFrom { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You have to choose an option")]
        public int AccountIdTo { get; set; }

        public int CustomerId { get; set; }       

        public List<SelectListItem> Accounts { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int customerId)
        {
            if (customerId == null && _signInManager.IsSignedIn(User))
            {
                var UserEmail = User.FindFirstValue(ClaimTypes.Email);
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.EmailAddress == UserEmail);

                if (customer != null)
                {
                    CustomerId = customer.Id;
                    GetAccounts(customer.Id);
                    
                    return Page();
                }
                else
                {
                    return Page();
                }

            }
            else if (User.IsInRole("Admin"))
            {
                GetAccounts(customerId);
                CustomerId = (int)customerId;
                return Page();
            }
            else
            {
                return LocalRedirect("/Identity/Account/AccessDenied");

            }
            
        }

        private List<SelectListItem> GetAccounts(int? customerId)
        {
            Accounts = _transactionService.GetAllAccounts(customerId)
                   .Select(a => new SelectListItem
                   {
                       Value = a.Id.ToString(),
                       Text = a.AccountType,

                   }).ToList();
            return Accounts;
        }

        public IActionResult OnGetFetchInfo(int accountid)
        {
            var account = _transactionService.GetAccount(accountid);   

            return new JsonResult(new
            {
                balance = account.Balance,
                currentaccountid = account.Id
            });

        }
        public async Task<IActionResult> OnPostUpdateAsync(int accountIdfrom, int accountIdto, int amount, int customerId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _transactionService.TransferAsync(accountIdfrom, accountIdto, amount);
                    return RedirectToPage("./Transactions", new { customerId = customerId, accountId = accountIdfrom });

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                    CustomerId = customerId;
                    GetAccounts(customerId);
                    return Page();
                }
                
            }

            CustomerId = customerId;
            GetAccounts(customerId);
            return Page();


        }
    }
}
