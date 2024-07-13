using AutoMapper; 
using EmployeeApi.DTO;
using EmployeeApi.Models;
using EmployeeApi.Repository;

namespace EmployeeApi.Services
{
    public class EmployeeServices : IEmployeeRepository
    {
        private readonly EmployeeDbContext DB;
        IMapper Mapper;

        public EmployeeServices(EmployeeDbContext db,IMapper mper1)
        {
            DB = db;
            Mapper = mper1;
        }
        public List<EmployeeDto> GetEmployees()
        {
             List<Employees> EMP = DB.employees.ToList();
            List<EmployeeDto> employeeDtos = Mapper.Map<List<EmployeeDto>>(EMP);
            return employeeDtos;
        }

        public EmployeeDto GetEmployees(int id)
        {
            Employees EMP = DB.employees.First(x=>x.Id==id);
            EmployeeDto employeeDtos = Mapper.Map<EmployeeDto>(EMP);
            return employeeDtos;
        }

        public EmployeeDto InsertEmployee(EmployeeDto employeeDto)
        {
            Employees emp = Mapper.Map<Employees>(employeeDto);
            DB.employees.Add(emp);
            DB.SaveChanges();
            EmployeeDto dto = Mapper.Map<EmployeeDto>(emp);
            return dto;
        }

        //public Employees InsertEmployee(Employees emp)
        //{

        //    DB.employees.Add(emp);
         //    DB.SaveChanges();

        //    return emp;
        //}
    }
}
