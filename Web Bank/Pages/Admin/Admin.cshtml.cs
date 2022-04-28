using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Web_Bank.Data;
using Web_Bank.Data.IdentityManager.Admin;

namespace Web_Bank.Pages.Admin
{
    //[Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<RoleView> AllRoles { get; set; }

        public void OnGet()
        {
            AllRoles = _dbContext.Roles.Select(r => new RoleView
            {
                Id = r.Id,
                Name = r.Name,
                NormalizedName = r.NormalizedName
            }).ToList();            
        }
        public IActionResult OnPostCreate([Required] string name)
        {
            if (ModelState.IsValid)
            {
                var role = _dbContext.Roles.FirstOrDefault(r => r.Name == name);
                if (role == null)
                {
                    _dbContext.Roles.Add(new IdentityRole
                    {
                        Name = name,
                        NormalizedName = name.ToUpper()
                    });

                    _dbContext.SaveChanges();
                    return RedirectToPage();
                }
            }
            return Page();
        }
        public IActionResult OnPostDelete(string id)
        {
            var role =  _dbContext.Roles.Find(id);
            if (role != null)
            {
                var result =  _dbContext.Roles.Remove(role);
                _dbContext.SaveChanges();
                return RedirectToPage();

            }
            else
                ModelState.AddModelError("", "No role found");
            return Page();
        }
    }
}
