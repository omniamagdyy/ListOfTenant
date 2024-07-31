using ListOfTenants.Models;
using ListOfTenants.Validations;

public class UserRolesService : IUserRolesService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleUserRepository _roleUserRepository;

    public UserRolesService(IUserRepository userRepository, IRoleRepository roleRepository, IRoleUserRepository roleUserRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _roleUserRepository = roleUserRepository;
    }

    public Dictionary<string, Dictionary<string, string>> MapUsersToTenants(Dictionary<string, List<string>> userTenantMappings)
    {
        var results = new Dictionary<string, Dictionary<string, string>>();

        foreach (var kvp in userTenantMappings)
        {
            var username = kvp.Key;
            var tenantIds = kvp.Value;
            var tenantResults = new Dictionary<string, string>();

            var user = _userRepository.GetUserByUsername(username);
            if (user == null || !user.IsActive)
            {
                tenantResults[username] = RoleUserValidations.invaildUsername.ToString();
                results[username] = tenantResults;
                continue;
            }
            
            foreach (var tenantId in tenantIds)
            {
                var validation = ValidateAndMapUserToTenant(username, tenantId);
                string message = validation.ToString();
                tenantResults[tenantId] = message;
            }
            results[username] = tenantResults;

        }
        return results;

    }
    private RoleUserValidations ValidateAndMapUserToTenant(string username, string tenantId)
    {
        var user = _userRepository.GetUserByUsername(username);
        if (user == null || !user.IsActive)
        {
            return RoleUserValidations.invaildUsername;
        }

        var tenants = _roleRepository.GetTenantById(tenantId);

        if (tenants == null)
        {
            return RoleUserValidations.ThisTenantsDoesNotExist;
        }

        var roles = _roleRepository.GetRolesByTenantIds(new List<string> { tenantId });
        
        var roleIds = roles.Select(i => i.Id).ToList();
        if (!_roleUserRepository.UserHasRole(user.Id, roleIds))
        {
            var defaultRole = CreateDefaultRole(username, tenantId);
            _roleRepository.AddRole(defaultRole);
            _roleRepository.Save();
            
                    var userRole = new RoleUser
                    {
                        UsersId = user.Id,
                        RolesId = defaultRole.Id
                    };

                    _roleUserRepository.AddUserRole(userRole);
                    _roleUserRepository.Save();
                    return RoleUserValidations.success;
        }
            return RoleUserValidations.alreadyHaveThisTenants;
    }
    private Aspnetrole CreateDefaultRole(string username, string tenantId)
    {
        return new Aspnetrole
        {
            Id = Guid.NewGuid().ToString(),
            Description = $"{username}_{tenantId}",
            TenantId = tenantId,
            Name = $"default_{tenantId}",
            NormalizedName = $"DEFAULT_{tenantId.ToUpper()}"
        };
    }
}