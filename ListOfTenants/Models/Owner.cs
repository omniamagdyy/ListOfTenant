using System;
using System.Collections.Generic;

namespace ListOfTenants.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Aspnetusers = new HashSet<Aspnetuser>();
            Tenants = new HashSet<Tenant>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsVirtual { get; set; }

        public virtual ICollection<Aspnetuser> Aspnetusers { get; set; }
        public virtual ICollection<Tenant> Tenants { get; set; }
    }
}
