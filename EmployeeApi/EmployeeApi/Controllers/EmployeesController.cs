using EmployeeApi.DTO;
using AutoMapper;
using EmployeeApi.Repository;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository Repository;
        private ResponseDto response;

        public EmployeesController(IEmployeeRepository repository)
        {
            Repository = repository;
            response= new ResponseDto();
        }
        [HttpGet]

         public ResponseDto GetEmployee()
         {
            List<EmployeeDto> employees = Repository.GetEmployees();
            response.Result = employees;
            return response;
         }
        [HttpGet("GetEmployeeById")]

        public ResponseDto GetEmployeeById(int id)
        {
            EmployeeDto employees = Repository.GetEmployees(id);
            response.Result = employees;
            return response;
        }

        [HttpPost]

        public ResponseDto InsertEmployee([FromBody]EmployeeDto empdto)
        {
            try
            {
                if (!string.IsNullOrEmpty(empdto.Name) && !string.IsNullOrEmpty(empdto.Salary))
                {
                    EmployeeDto dto = Repository.InsertEmployee(empdto);
                    response.Result = dto;
                }
                else
                {
                    response.Message = "Data Souldn't be Empty";
                }
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message= ex.Message;
            }
            return response;
        }
    }
}
