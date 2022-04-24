using Web_Bank.Data;

namespace Web_Bank.Services
{
    public interface IAccountTransactionService
    {
        List<Account> GetAllAccounts(int? customerId);
        Account GetAccount(int accountId);
        Task WithdrawAsync(int accountId, int belopp);
        bool CanWithdraw(int accountId, int amount);
        bool CanDeposit(string operation, int accountId, int amount);
        Task DepositAsync(string operation, int accountId, int amount);
    }
}