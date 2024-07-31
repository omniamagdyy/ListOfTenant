using ListOfTenants.Models;

public interface IUserRepository
{
    
    Aspnetuser GetUserByUsername(string username);
    
    Aspnetuser GetUserByID(string id);
    void Save();


}



