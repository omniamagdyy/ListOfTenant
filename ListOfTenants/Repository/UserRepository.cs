using System.Linq;
using Microsoft.AspNetCore.Identity;
using ListOfTenants.Models;
using ListOfTenants.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly wesidentityContext _context;
    private readonly UserManager<Aspnetuser> _userManager;

    public UserRepository(wesidentityContext context, UserManager<Aspnetuser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
 

    public Aspnetuser GetUserByUsername(string username )
    {
        return _context.Aspnetusers.Include(u => u.Roles).SingleOrDefault(u => u.UserName == username && u.IsActive);

    }
    public Aspnetuser GetUserByID(string id)
    {
        return _context.Aspnetusers.Include(u => u.Roles).FirstOrDefault(u => u.Id == id);
    }
        
    
    public void Save()
    {
        _context.SaveChanges();
    }


}

