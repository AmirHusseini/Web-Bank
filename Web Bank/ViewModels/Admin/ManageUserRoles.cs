﻿using System.ComponentModel.DataAnnotations;

namespace Web_Bank.Data.IdentityManager.Admin
{
    public class ManageUserRoles
    {
        public string RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
