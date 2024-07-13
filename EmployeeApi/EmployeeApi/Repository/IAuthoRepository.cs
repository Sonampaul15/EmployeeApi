 using EmployeeApi.DTO;

namespace EmployeeApi.Repository
{
    public interface IAuthoRepository
    {
        Task<string> RegisterAsync(RegRequestDto reg);

        Task<LoginResponseDto> LoginAsync(LoginRequestDto login);

        Task<bool> AssignRoleAsync(string email, string rolename);
    }
}
