using System;
using System.Collections.Generic;

namespace ListOfTenants.Models
{
    public partial class Tenant
    {
        public Tenant()
        {
            Aspnetroles = new HashSet<Aspnetrole>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Logo { get; set; }
        public bool IsActive { get; set; }
        public bool IsVirtual { get; set; }
        public string? OwnerId { get; set; }
        public string CountryCode { get; set; } = null!;

        public virtual Owner? Owner { get; set; }
        public virtual ICollection<Aspnetrole> Aspnetroles { get; set; }
    }
}
