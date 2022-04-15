using System.ComponentModel.DataAnnotations;

namespace Web_Bank.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Givenname { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        public string Telephone { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
    }
}
