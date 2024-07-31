using System.Collections.Generic;
using System.Linq;
using ListOfTenants.Data;
using ListOfTenants.Models;
using Microsoft.EntityFrameworkCore;

public class RoleRepository : IRoleRepository
{
    private readonly wesidentityContext _context;

    public RoleRepository(wesidentityContext context)
    {
        _context = context;
    }

    public Aspnetrole GetRoleById(string id)
    {
        return _context.Aspnetroles.Find(id);
    }
    public Tenant GetTenantById(string id)
    {
        return _context.Tenants.Find(id);
    }

    public ICollection<Aspnetrole> GetRolesByTenantIds(ICollection<string> tenantIds)
    {
        return _context.Aspnetroles.Include(r => r.Users).Where(r => tenantIds.Contains(r.TenantId)).ToList();
    }
    public void AddRole(Aspnetrole role)
    {
        _context.Aspnetroles.Add(role);
        _context.SaveChanges();
    }
    public void Save()
        { _context.SaveChanges(); }


   

}
