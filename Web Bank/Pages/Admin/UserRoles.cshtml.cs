using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_Bank.Data;
using Web_Bank.Data.IdentityManager.Admin;

namespace Web_Bank.Pages.Admin
{
    public class UserRolesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;


        public UserRolesModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }
        public List<UserRolesView> userRolesViews { get; set; } = new List<UserRolesView>();
        public async Task<IActionResult> OnGet()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (IdentityUser user in users)
            {
                var thisviewModel = new UserRolesView();
                thisviewModel.UserId = user.Id;
                thisviewModel.UserName = user.UserName;
                thisviewModel.Email = user.Email;
                thisviewModel.Roles = await GetUserRoles(user);
                userRolesViews.Add(thisviewModel);

            }

            return Page();
        }
        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}
