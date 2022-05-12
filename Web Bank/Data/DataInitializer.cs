using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_Bank.Data.IdentityManager;

namespace Web_Bank.Data;

public class DataInitializer
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;


    public DataInitializer(ApplicationDbContext dbContext,
        UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }


    public void SeedData()
    {
        _dbContext.Database.Migrate();
        SeedCustomers();
        SeedRoles();
        SeedUsers();

    }

    private void SeedCustomers()
    {
        while (_dbContext.Customers.Count() < 5000)
        {
            var a = GenerateCustomer();
            _dbContext.Customers.Add(a);
            _dbContext.SaveChanges();
        }

    }
    private void SeedUsers()
    {
        AddUserIfNotExists("stefan.holmberg@systementor.se", "Hejsan123#", new string[] { "Admin" });
        AddUserIfNotExists("stefan.holmberg@customer.systementor.se", "Hejsan123#", new string[] { "Cashier" });
        AddUserIfNotExists("Susie_Karlsson@yahoo.com", "Susie_Karlsson@1", new string[] { "Customer" });
    }


    private void SeedRoles()
    {
        AddRoleIfNotExisting("Admin");
        AddRoleIfNotExisting("Customer");
        AddRoleIfNotExisting("Cashier");
    }

    private void AddRoleIfNotExisting(string roleName)
    {
        var role = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
        if (role == null)
        {
            _dbContext.Roles.Add(new IdentityRole
            { 
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            });
                
            _dbContext.SaveChanges();
        }
    }

    private void AddUserIfNotExists(string userName, string password, string[] roles)
    {
        if (_userManager.FindByEmailAsync(userName).Result != null) return;

        var user = new IdentityUser
        {
            UserName = userName,
            Email = userName,
            EmailConfirmed = true
        };
        
        _userManager.CreateAsync(user, password).Wait();
        _userManager.AddToRolesAsync(user, roles).Wait();
    }
    
    private static Random random = new Random();
    private Customer GenerateCustomer()
    {
        // f.Date.Between(new DateTime(1999,1,1), new DateTime(1940,1,1))
        var n = random.Next(0, 100);
        Customer person = null;
        if (n < 20)
        {
            var testUser = new Faker<Customer>("nb_NO")
                .StrictMode(false)
                .RuleFor(e => e.Id, f => 0)
                .RuleFor(e => e.City, (f, u) => f.Address.City())
                .RuleFor(e => e.Country, (f, u) => "Norge")
                .RuleFor(e => e.CountryCode, (f, u) => "NO")
                .RuleFor(e => e.Birthday, (f, u) => f.Person.DateOfBirth)
                .RuleFor(e => e.EmailAddress, (f, u) => f.Internet.Email())
                .RuleFor(e => e.Givenname, (f, u) => f.Person.FirstName)
                .RuleFor(e => e.Surname, (f, u) => f.Person.LastName)
                .RuleFor(e => e.NationalId, (f, u) => f.Person.DateOfBirth.ToString("yyyyMMdd") + "-3333")
                .RuleFor(e => e.Streetaddress, (f, u) => f.Address.StreetAddress())
                .RuleFor(e => e.Telephone, (f, u) => f.Person.Phone)
                .RuleFor(e => e.Zipcode, (f, u) => f.Address.ZipCode())
                .RuleFor(e => e.TelephoneCountryCode, (f, u) => 47);
            person = testUser.Generate(1).First();
        }

        else if (n < 80)
        {
            var testUser = new Faker<Customer>("sv")
                .StrictMode(false)
                .RuleFor(e => e.Id, f => 0)
                .RuleFor(e => e.City, (f, u) => f.Address.City())
                .RuleFor(e => e.Country, (f, u) => "Sverige")
                .RuleFor(e => e.CountryCode, (f, u) => "SE")
                .RuleFor(e => e.Birthday, (f, u) => f.Person.DateOfBirth)
                .RuleFor(e => e.EmailAddress, (f, u) => f.Internet.Email())
                .RuleFor(e => e.Givenname, (f, u) => f.Person.FirstName)
                .RuleFor(e => e.Surname, (f, u) => f.Person.LastName)
                .RuleFor(e => e.NationalId, (f, u) => f.Person.DateOfBirth.ToString("yyyyMMdd") + "-1111")
                .RuleFor(e => e.Streetaddress, (f, u) => f.Address.StreetAddress())
                .RuleFor(e => e.Telephone, (f, u) => f.Person.Phone)
                .RuleFor(e => e.Zipcode, (f, u) => f.Address.ZipCode())
                .RuleFor(e => e.TelephoneCountryCode, (f, u) => 46);
            person = testUser.Generate(1).First();
        }


        else 
        {
            var testUser = new Faker<Customer>("fi")
                .StrictMode(false)
                .RuleFor(e => e.Id, f => 0)
                .RuleFor(e => e.City, (f, u) => f.Address.City())
                .RuleFor(e => e.Country, (f, u) => "Finland")
                .RuleFor(e => e.CountryCode, (f, u) => "FI")
                .RuleFor(e => e.Birthday, (f, u) => f.Person.DateOfBirth)
                .RuleFor(e => e.EmailAddress, (f, u) => f.Internet.Email())
                .RuleFor(e => e.Givenname, (f, u) => f.Person.FirstName)
                .RuleFor(e => e.Surname, (f, u) => f.Person.LastName)
                .RuleFor(e => e.NationalId, (f, u) => f.Person.DateOfBirth.ToString("yyyyMMdd") + "-2222")
                .RuleFor(e => e.Streetaddress, (f, u) => f.Address.StreetAddress())
                .RuleFor(e => e.Telephone, (f, u) => f.Person.Phone)
                .RuleFor(e => e.Zipcode, (f, u) => f.Address.ZipCode())
                .RuleFor(e => e.TelephoneCountryCode, (f, u) => 48);
            person = testUser.Generate(1).First();
        }

        for (int i = 0; i < random.Next(1, 5);i++)
        {
            person.Accounts.Add(GenerateAccount());
        }

        


        return person;
    }


    private Account GenerateAccount()
    {
        string[] accountType = {"Personal", "Checking", "Savings"};
        var testUser = new Faker<Account>()
            .StrictMode(false)
            .RuleFor(e => e.Id, f => 0)
            .RuleFor(e => e.AccountType, (f, u) => f.PickRandom(accountType))
            .RuleFor(e => e.Balance, (f, u) => 0);

        var account = testUser.Generate(1).First();
        var start = DateTime.Now.AddDays(-random.Next(1000, 10000));
        account.Created = start;
        account.Balance = 0;
        var transactions = random.Next(1, 30);
        for (int i = 0; i < transactions; i++)
        {
            var tran = new Transaction();
            tran.Amount = random.NextInt64(1, 50) * 100;
            start = start.AddDays(random.NextInt64(50, 600));
            if (start > DateTime.Now)
                break;
            tran.Date = start;
            account.Transactions.Add(tran);
            if (account.Balance - tran.Amount < 0)
                tran.Type = "Debit";
            else
            {
                if (random.NextInt64(0, 100) > 70)
                    tran.Type = "Debit";
                else
                    tran.Type = "Credit";
            }

            var r = random.Next(0, 100);
            if (tran.Type == "Debit")
            {
                account.Balance = account.Balance + tran.Amount;
                if (r < 20)
                    tran.Operation = "Deposit cash";
                else if(r < 66)
                    tran.Operation = "Salary";
                else
                    tran.Operation = "Transfer";
            }
            else
            {
                account.Balance = account.Balance - tran.Amount;
                if (r < 40)
                    tran.Operation = "ATM withdrawal";
                else if (r < 66)
                    tran.Operation = "Payment";
                else
                    tran.Operation = "Transfer";
            }

            tran.NewBalance = account.Balance;
        }
        return account;
    }
}