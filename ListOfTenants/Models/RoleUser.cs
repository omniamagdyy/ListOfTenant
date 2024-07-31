using System.ComponentModel.DataAnnotations.Schema;

namespace ListOfTenants.Models
{
    public class RoleUser
    {
        [ForeignKey("Role")]
        public string RolesId { get; set; }
        [ForeignKey("User")]
        public string UsersId { get; set; }
        public Aspnetuser User { get; set; }
        public Aspnetuser Role { get; set; }

    }
}
