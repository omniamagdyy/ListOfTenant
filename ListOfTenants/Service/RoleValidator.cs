using ListOfTenants.Models;
using ListOfTenants.Validations;

public class RoleValidator
{
    private readonly IRoleRepository _roleRepository;

    public RoleValidator(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public RoleUserValidations ValidateAndCreate(string tenantId, out Aspnetrole defaultRole)
    {
        var roles = _roleRepository.GetRolesByTenantIds(new List<string> { tenantId });
        defaultRole = roles.FirstOrDefault();

        if (defaultRole == null)
        {
            defaultRole = new Aspnetrole
            {
                Id = Guid.NewGuid().ToString(),
                Description = $"Default role for tenant {tenantId}",
                TenantId = tenantId,
                Name = $"default_{tenantId}",
                NormalizedName = $"DEFAULT_{tenantId.ToUpper()}"
            };
            _roleRepository.AddRole(defaultRole);
            return RoleUserValidations.success;
        }

        return RoleUserValidations.success;
    }

    public ICollection<Aspnetrole> GetRolesByTenantIds(ICollection<string> tenantIds)
    {
        return _roleRepository.GetRolesByTenantIds(tenantIds);
    }
}

