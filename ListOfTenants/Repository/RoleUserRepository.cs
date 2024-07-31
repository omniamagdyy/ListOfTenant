using System.Linq;
using ListOfTenants.Data;
using ListOfTenants.Models;

public class RoleUserRepository : IRoleUserRepository
{
    private readonly wesidentityContext _context;

    public RoleUserRepository(wesidentityContext context)
    {
        _context = context;
    }

    public void AddUserRole(RoleUser userRole)
    {
        _context.RoleUsers.Add(userRole);
    }

    public RoleUser GetUserRole(string userId, string roleId)
    {
        return _context.RoleUsers.SingleOrDefault(ur => ur.UsersId == userId && ur.RolesId == roleId);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
    public bool UserHasRole(string userId, List<string> roleId)
    {
        
        return _context.RoleUsers.Any(ur => ur.UsersId == userId && roleId.Contains(ur.RolesId));
    }


}

