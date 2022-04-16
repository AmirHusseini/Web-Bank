using Web_Bank.Data;

namespace Web_Bank.Services
{
    public interface IAccountTransactionService
    {
        public List<Customer> GetAllCustomers();
        void Update(Account account);
        public List<Account> GetAccounts(int id);
    }
}