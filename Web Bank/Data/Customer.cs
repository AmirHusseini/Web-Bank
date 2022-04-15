using System.ComponentModel.DataAnnotations;

namespace Web_Bank.Data;

public class Customer
{
    public int Id { get; set; }
    [MaxLength(50)] 
    public string Givenname { get; set; }
    [MaxLength(50)] 
    public string Surname { get; set; }
    [MaxLength(50)] 
    public string Streetaddress { get; set; }
    [MaxLength(50)] 
    public string City { get; set; }
    [MaxLength(10)] 
    public string Zipcode { get; set; }
    [MaxLength(30)] 
    public string Country { get; set; }
    [MaxLength(2)] 
    public string CountryCode { get; set; }
    [MaxLength(20)] 
    public string NationalId { get; set; }
    [Range(0, 9999)]
    public int TelephoneCountryCode { get; set; }
    public string Telephone { get; set; }
    [MaxLength(50)]
    public string EmailAddress { get; set; }
    public DateTime Birthday { get; set; }

    public List<Account> Accounts { get; set; } = new List<Account>();

}