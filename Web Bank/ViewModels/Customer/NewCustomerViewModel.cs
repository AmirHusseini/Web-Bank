using System.ComponentModel.DataAnnotations;

namespace Web_Bank.ViewModels
{
    public class NewCustomerViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Given Name")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Givenname { get; set; }

        
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Surname { get; set; }

        [Display(Name = "Street Address")]
        [Required]
        [MaxLength(50)]
        public string Streetaddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        [Required]
        [MaxLength(10)]
        public string Zipcode { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Display(Name = "Country Code")]
        [Required]
        [StringLength(5, MinimumLength = 2)]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string NationalId { get; set; }

        [Display(Name = "Telephone Country Code")]
        [Required]
        [StringLength(6, MinimumLength = 1)]
        public int TelephoneCountryCode { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
    }
}
