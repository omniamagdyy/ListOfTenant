﻿using System;
using System.Collections.Generic;

namespace ListOfTenants.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Aspnetrole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string EnNormalizedName { get; set; } = null!;

        public virtual ICollection<Aspnetrole> Roles { get; set; }
    }
}
