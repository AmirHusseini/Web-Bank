using System.ComponentModel.DataAnnotations;

namespace Web_Bank.Data;

public class Account
{
    public int Id { get; set; }


    [MaxLength(10)]
    public string AccountType { get; set; }

    public DateTime Created { get; set; }
    public decimal Balance { get; set; }

    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}