using Web_Bank.Data;

namespace Web_Bank.Services
{
    public interface IAccountTransactionService
    {
        List<Account> GetAllAccounts(int? customerId);
        Account GetAccount(int accountId);
        bool CanWithdraw(int accountId, int amount);
        Task WithdrawAsync(int accountId, int belopp);        
        bool CanDeposit(string operation, int accountId, int amount);
        Task DepositAsync(string operation, int accountId, int amount);
        bool CanTransfer(int accountIdfrom, int accountIdto, int amount);
        Task TransferAsync(int accountIdfrom, int accountIdto, int amount);
    }
}