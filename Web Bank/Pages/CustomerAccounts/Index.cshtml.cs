using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Web_Bank.Data;
using Web_Bank.Services;

namespace Web_Bank.Pages.CustomerAccounts
{
    public class IndexModel : PageModel
    {
        private readonly AccountTransactionService _account;

        public IndexModel(AccountTransactionService account)
        {
            _account = account;
        }
        public List<Data.Customer> customers { get; set; }
        public void OnGet()
        {
            customers = _account.GetAllCustomers();
        }
    }
}
