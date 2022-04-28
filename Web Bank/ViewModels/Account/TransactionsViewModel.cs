using System.ComponentModel.DataAnnotations;

namespace Web_Bank.ViewModels
{
    public class TransactionsViewModel
    {
        public int Id { get; set; }
        
        public string Type { get; set; }
        
        public string Operation { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
    }
}
