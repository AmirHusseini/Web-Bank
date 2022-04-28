using System.ComponentModel.DataAnnotations;

namespace Web_Bank.ViewModels.Customer
{
    public class RemoveCustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Given Name")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Givenname { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string NationalId { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

    }
}
