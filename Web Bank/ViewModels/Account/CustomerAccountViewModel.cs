using System.ComponentModel.DataAnnotations;
using Web_Bank.Data;

namespace Web_Bank.ViewModels
{
    public class CustomerAccountViewModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Givenname { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string photo { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        
    }
}
