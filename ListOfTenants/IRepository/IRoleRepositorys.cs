using System.Collections.Generic;
using ListOfTenants.Models;


public interface IRoleRepository
{
    Aspnetrole GetRoleById(string id);

    ICollection<Aspnetrole> GetRolesByTenantIds(ICollection<string> tenantIds);
    Tenant GetTenantById(string id);
    void AddRole(Aspnetrole role);
    void Save();
    
}




