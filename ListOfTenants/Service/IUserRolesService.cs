using ListOfTenants.Validations;
using System.Collections.Generic;

public interface IUserRolesService
{
        Dictionary<string, Dictionary<string, string>> MapUsersToTenants(Dictionary<string, List<string>> userTenantMappings);
   
}




