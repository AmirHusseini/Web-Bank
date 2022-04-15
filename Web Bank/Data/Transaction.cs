using System.ComponentModel.DataAnnotations;

namespace Web_Bank.Data;

public class Transaction
{
    public int Id { get; set; }
    [MaxLength(10)]
    public string Type { get; set; }
    [MaxLength(50)]
    public string Operation { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public decimal NewBalance { get; set; }
}