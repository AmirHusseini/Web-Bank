using Web_Bank.Data;

namespace Web_Bank.Services
{
    public interface IAccountTransactionService
    {
        List<Account> GetAllAccounts(int? customerId);
        Account GetAccount(int accountId);
        
        Task WithdrawAsync(int accountId, int belopp);        

        Task DepositAsync(string operation, int accountId, int amount);

        Task TransferAsync(int accountIdfrom, int accountIdto, int amount);
    }
}