using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Web_Bank.Data;
using Web_Bank.Data.IdentityManager.Admin;

namespace Web_Bank.Pages.Admin
{
    [BindProperties]
    public class ManageUserRolesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageUserRolesModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public string userId { get; set; }
        [BindProperty]
        public List<ManageUserRoles> userRoles { get; set; }
        public async Task OnGetAsync(string userId)
        {

            this.userRoles = await GetAllAsync(userId);
            
        }

        public async Task<IActionResult> OnPostUpdate(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Page();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return Page();
            }
            result = await _userManager.AddToRolesAsync(user, userRoles.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return Page();
            }
            return RedirectToPage("./UserRoles", userId);
        }
        public async Task<List<ManageUserRoles>> GetAllAsync(string nuserId)
        {
            var items = new List<ManageUserRoles>();
            userId = nuserId;
            var user = await _userManager.FindByIdAsync(nuserId);
            if (user != null)
            {


            }

            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRoles
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                items.Add(userRolesViewModel);
            }
            return items;
        }
    }
}
