using EmployeeApi.Models;

namespace EmployeeApi.Repository
{
    public interface IJwtTokenGenerator
    {
       string GenerateToken(ApplicationUser application,IEnumerable<String>roles);
    }
}
