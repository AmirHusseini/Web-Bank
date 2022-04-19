using System.ComponentModel.DataAnnotations;
using Web_Bank.Data;

namespace Web_Bank.ViewModels
{
    public class CustomersViewModel
    {
        public int Id { get; set; }

        public string Givenname { get; set; }

        public string Surname { get; set; }

        public string NationalId { get; set; }

        public string Streetaddress { get; set; }

        public string City { get; set; }        
       
    }
}
