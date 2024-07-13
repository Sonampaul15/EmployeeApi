using EmployeeApi.DTO;
using EmployeeApi.Models;
using EmployeeApi.Repository;
using Microsoft.AspNetCore.Identity;

namespace EmployeeApi.Services
{
    public class AuthService : IAuthoRepository
    {
        private readonly EmployeeDbContext DB;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AuthService(EmployeeDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator _jwtTokenGenerator)
        {
            DB= db;
            UserManager= userManager;
            RoleManager= roleManager;
            jwtTokenGenerator = _jwtTokenGenerator;
        }  
        public async Task<bool> AssignRoleAsync(string email, string rolename)
        {
            var user = DB.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            if (user != null)   
            {
                if(!RoleManager.RoleExistsAsync(rolename).GetAwaiter().GetResult())
                {
                    RoleManager.CreateAsync(new IdentityRole(rolename)).GetAwaiter().GetResult();
                }
                await UserManager.AddToRoleAsync(user,rolename);
                return true;
            }
            return false;
        }      

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto login)
        {
            var user= DB.Users.FirstOrDefault(x => x.UserName.ToLower()==login.UserName.ToLower());
            bool isValid= await UserManager.CheckPasswordAsync(user, login.Password);
            if (user == null || isValid == false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = "",
                };
            }
            var roles = await UserManager.GetRolesAsync(user);
            var token = jwtTokenGenerator.GenerateToken(user, roles);
            UserDto dto = new UserDto()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.FirstName,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponse = new LoginResponseDto()
            {
                User = dto,
                Token = token
            };
            return loginResponse;
        }

        public async Task<string> RegisterAsync(RegRequestDto reg)
        {
            ApplicationUser Users = new ApplicationUser()
            {
                FirstName = reg.Name,
                LastName = reg.LastName,
                Gender = reg.Gender,
                UserName = reg.Email,
                Email = reg.Email,
                NormalizedEmail = reg.Email,
                PhoneNumber = reg.PhoneNumber,
            };
            try
            {
                var result = await UserManager.CreateAsync(Users, reg.Password);
                if (result.Succeeded)
                {
                    var returnUserData = DB.Users.First(x=> x.Email== reg.Email);
                    UserDto dto = new UserDto()
                    {
                        Email = returnUserData.Email,
                        Id = returnUserData.Id,
                        Name = returnUserData.UserName,
                        PhoneNumber = returnUserData.PhoneNumber,
                       };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
