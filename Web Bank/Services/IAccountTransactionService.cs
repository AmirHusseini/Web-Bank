using Web_Bank.Data;

namespace Web_Bank.Services
{
    public interface IAccountTransactionService
    {
        public List<Account> GetAllAccounts();
        void Update(Account account);
        public Account GetAccount(int accountId);
        public bool Withdraw(int accountId, int belopp);
    }
}