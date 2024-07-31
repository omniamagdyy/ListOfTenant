using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRolesService _userRolesService;

    public UsersController(IUserRolesService userRolesService)
    {
        _userRolesService = userRolesService;
    }

    [HttpPost("mapUsersToTenants")]
    public IActionResult MapUsersToTenants([FromBody] Dictionary<string, List<string>> userTenantMappings)
    {
        var results = _userRolesService.MapUsersToTenants(userTenantMappings);
        return Ok(results);
    }
}
