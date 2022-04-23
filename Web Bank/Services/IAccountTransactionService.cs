using Web_Bank.Data;

namespace Web_Bank.Services
{
    public interface IAccountTransactionService
    {
        public List<Account> GetAllAccounts();
        public Account GetAccount(int accountId);
        public void Withdraw(int accountId, int belopp);
        public bool CanWithdraw(int accountId, int amount);
    }
}