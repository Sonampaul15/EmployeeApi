using Microsoft.AspNetCore.Identity;

namespace EmployeeApi.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }  = string.Empty;

        public string LastName { get; set; }= string.Empty;

        public string Gender { get; set; } = string.Empty;
    }
}
