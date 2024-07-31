using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ListOfTenants.Models
{
    public partial class Aspnetuser :  IdentityUser
    {
        public Aspnetuser()
        {
            Aspnetusertokens = new HashSet<Aspnetusertoken>();
            Roles = new HashSet<Aspnetrole>();
            RolesNavigation = new HashSet<Aspnetrole>();
        }

        public string Id { get; set; } = null!;
        public string? FullName { get; set; }
        public string OwnerId { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int? LoginCount { get; set; }
        public bool IsActive { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual Owner Owner { get; set; } = null!;
        public virtual ICollection<Aspnetusertoken> Aspnetusertokens { get; set; }

        public virtual ICollection<Aspnetrole> Roles { get; set; }
        public virtual ICollection<Aspnetrole> RolesNavigation { get; set; }
    }
}
