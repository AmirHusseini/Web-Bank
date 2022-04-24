using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Web_Bank.Services;

namespace Web_Bank.Pages.CustomerAccounts
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly IAccountTransactionService _transactionService;
        public DepositModel(IAccountTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Range(5, double.MaxValue, ErrorMessage = "The minimum withdraw has to be 5$")]
        public Decimal Amount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You have to choose an option")]
        public int AccountId { get; set; }

        public string OperationId { get; set; }
        public int CustomerId { get; set; }

        public List<SelectListItem> Accounts { get; set; }
        public List<SelectListItem> Operation { get; set; }

        public void OnGet(int customerId)
        {
            GetAccounts(customerId);
            GetOperations();
        }

        private List<SelectListItem> GetOperations()
        {
            Operation = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(op)))
            {
                Operation.Add(new SelectListItem()
                {
                    Value = item.ToString(),
                    Text = item.ToString()
                });
            }
            return Operation;
        }
        public enum op
        {
            Transfer, 
            Salary, 
            [Display(Name = "Deposit Cash")]
            Depositcash
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
            if (ModelState.IsValid)
            {
                if (_transactionService.CanDeposit(operationId, accountId, amount))
                {
                    await _transactionService.DepositAsync(operationId, accountId, amount);
                    return RedirectToPage("./Transactions", new { accountId = accountId });
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
