using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web_Bank.Pages.Admin
{
    public class AdminModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<IdentityRole> roles { get; set; }

        public void OnGet()
        {
            roles = _roleManager.Roles.ToList();
        }

        public async Task<IActionResult> OnPostCreate([Required] string name)
        {
            if (ModelState.IsValid)
            {
                if (name != null)
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                    if (result.Succeeded)
                        return RedirectToPage();
                }

            }
            return Page();
        }
        public async Task<IActionResult> OnPostDelete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToPage();

            }
            else
                ModelState.AddModelError("", "No role found");
            return Page();
        }
    }
}
