using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Web_Bank.Services;

namespace Web_Bank.Pages.CustomerAccounts
{
    [BindProperties]
    public class TransferModel : PageModel
    {
        private readonly IAccountTransactionService _transactionService;

        public TransferModel(IAccountTransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [Range(5, double.MaxValue, ErrorMessage = "The minimum transfer has to be 5$")]
        public Decimal Amount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You have to choose an option")]
        public int AccountIdFrom { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You have to choose an option")]
        public int AccountIdTo { get; set; }

        public int CustomerId { get; set; }       

        public List<SelectListItem> Accounts { get; set; }
        
        public void OnGet(int customerId)
        {            
            GetAccounts(customerId);
        }

        private List<SelectListItem> GetAccounts(int customerId)
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
                if (_transactionService.CanTransfer(accountIdfrom, accountIdto, amount))
                {
                    await _transactionService.TransferAsync(accountIdfrom, accountIdto, amount);
                    return RedirectToPage("./Transactions", new {customerId = customerId, accountId = accountIdfrom });
                }

            }
            else
            {
                GetAccounts(customerId);
                return Page();
            }
            return Page();


        }
    }
}
