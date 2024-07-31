using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListOfTenants.Models;
using System.Collections.Generic;
using System.Linq;
using ListOfTenants.Data;

/*namespace ListOfTenants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly wesidentityContext _context; 

        public TenantsController(wesidentityContext context)
        {
            _context = context;
        }

        
        [HttpPost("map")]
        public IActionResult MapTenants([FromBody] MapTenantsRequest request)
        {
            
            Aspnetuser user = _context.Aspnetusers
                .Include(u => u.Owner) 
                .FirstOrDefault(u => u.UserName == request.Username);

            if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash))
            {
                return BadRequest("Invalid credentials");
            }

            // Fetch owner from user
            Owner owner = user.Owner;

            if (owner == null)
            {
                return BadRequest("User is not associated with an owner");
            }

            // Create dummy tenants (if needed)
            List<Tenant> dummyTenants = new List<Tenant>
            {
                new Tenant { Name = "Dummy Tenant 1", IsActive = true, IsVirtual = false, OwnerId = owner.Id },
                new Tenant { Name = "Dummy Tenant 2", IsActive = true, IsVirtual = false, OwnerId = owner.Id },
                new Tenant { Name = "Dummy Tenant 3", IsActive = true, IsVirtual = false, OwnerId = owner.Id }
            };

            // Convert ICollection<Tenant> to List<Tenant> before adding range
            List<Tenant> ownerTenants = owner.Tenants.ToList();
            ownerTenants.AddRange(dummyTenants);

            // Update owner's tenants with the modified list
            owner.Tenants = ownerTenants;

            // Save changes to database
            _context.SaveChanges();

            return Ok("Mapped successfully");
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Implement your password verification logic here (e.g., using ASP.NET Core Identity UserManager)
            // Example:
            // var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(null, storedHash, password);
            // return passwordVerificationResult == PasswordVerificationResult.Success;

            // For demonstration purposes, assuming plain comparison
            return password == storedHash;
        }
    }
}
*/