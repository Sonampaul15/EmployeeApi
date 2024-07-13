using EmployeeApi.DTO;
using EmployeeApi.Repository;
using EmployeeApi.Services; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthoRepository authoRepository;
        protected ResponseDto response;

        public AuthenticationController(IAuthoRepository repository)
        {
            authoRepository = repository;
            response = new ResponseDto();
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegRequestDto regRequest)
        {
            var message = await authoRepository.RegisterAsync(regRequest);
            if (!string.IsNullOrEmpty(message))
            {
                response.IsSuccess = false;
                response.Message = message;
                return BadRequest(message);
            }
            return Ok(response);

        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody]RegRequestDto regRequest)
        {
            var assignRoleSuccessful = await authoRepository.AssignRoleAsync(regRequest.Email, regRequest.Role);
            if (!assignRoleSuccessful)
            {
                response.IsSuccess = false;
                response.Message = "Error Has Been Encounterd";
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto login)
        {
            var loginResponse= await authoRepository.LoginAsync(login);
            if(loginResponse.User == null)
            {
                response.IsSuccess=false;
                response.Message = "";
                return BadRequest(response);
            }
             response.Result = loginResponse;
            return Ok(response);
        }
    }

    
}
