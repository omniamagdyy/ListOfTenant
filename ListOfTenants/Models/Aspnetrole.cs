using System;
using System.Collections.Generic;

namespace ListOfTenants.Models
{
    public partial class Aspnetrole
    {
        public Aspnetrole()
        {
            Permissions = new HashSet<Permission>();
            Users = new HashSet<Aspnetuser>();
            UsersNavigation = new HashSet<Aspnetuser>();
        }

        public string Id { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TenantId { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual Tenant Tenant { get; set; } = null!;

        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<Aspnetuser> Users { get; set; }
        public virtual ICollection<Aspnetuser> UsersNavigation { get; set; }
    }
}
