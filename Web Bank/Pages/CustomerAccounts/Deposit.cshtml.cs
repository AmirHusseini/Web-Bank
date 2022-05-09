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
    public class DepositModel : PageModel
    {
        private readonly IAccountTransactionService _transactionService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public DepositModel(ApplicationDbContext context, IAccountTransactionService transactionService, SignInManager<IdentityUser> signInManager)
        {
            _transactionService = transactionService;
            _signInManager = signInManager;
            _dbContext = context;
        }

        [Range(5, double.MaxValue, ErrorMessage = "The minimum withdraw has to be 5$")]
        public Decimal Amount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose an option")]
        public int AccountId { get; set; }

        
        public string OperationId { get; set; }

        public int CustomerId { get; set; }

        public List<SelectListItem> Accounts { get; set; }
        public List<SelectListItem> Operation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? customerId)
        {
            if (customerId == null && _signInManager.IsSignedIn(User))
            {
                var UserEmail = User.FindFirstValue(ClaimTypes.Email);
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.EmailAddress == UserEmail);

                if (customer != null)
                {
                    CustomerId = customer.Id;
                    GetAccounts(customer.Id);
                    GetOperations();
                    return Page();
                }
                else
                {
                    return Page();
                }
                
            }
            else if (User.IsInRole("Admin"))
            {

                CustomerId = (int)customerId;
                GetAccounts(customerId);
                GetOperations();
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
        private List<SelectListItem> GetOperations()
        {
            Operation = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(op)))
            {
                Operation.Add(new SelectListItem()
                {
                    Value = item.ToString(),
                    Text =  item.ToString(),
                    
                });
            }
            return Operation;
        }
        public enum op
        {
            Transfer = 1, 
            Salary = 2, 
            [Display(Name = "Deposit Cash")]
            Depositcash = 3
        }
        

        public IActionResult OnGetFetchInfo(int id)
        {
            var account = _transactionService.GetAccount(id);


            return new JsonResult(new
            {
                balance = account.Balance,
                currentaccountid = account.Id
            });

        }
        public async Task<IActionResult> OnPostUpdateAsync(string operationId, int accountId, int amount, int customerId)
        {
            if (operationId == "Select your operation")
            {
                ModelState.AddModelError("OperationId", "You must choose an option");
                GetAccounts(customerId);
                GetOperations();
                return Page();
            }
            if (ModelState.IsValid)
            {
                if (operationId == "Depositcash")
                {
                    operationId = "Deposit Cash";
                }
                if (_transactionService.CanDeposit(operationId, accountId, amount))
                {
                    await _transactionService.DepositAsync(operationId, accountId, amount);
                    return RedirectToPage("./Transactions", new { customerId = customerId, accountId = accountId });
                }

            }
            else
            {
                CustomerId = customerId;    
                GetAccounts(customerId);
                GetOperations();
                return Page();
            }
            return Page();


        }
    }
}
