namespace Web_Bank.Data.IdentityManager.Admin
{
    public class UserRolesView
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
