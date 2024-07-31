using ListOfTenants.Models;

    

    public interface IRoleUserRepository
    {
        void AddUserRole(RoleUser userRole);
        RoleUser GetUserRole(string userId, string roleId);
        void Save();
        bool UserHasRole(string userId, List<string> roleId);


}


