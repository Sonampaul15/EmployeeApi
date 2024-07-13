using EmployeeApi.DTO;

namespace EmployeeApi.Repository
{
    public interface IEmployeeRepository
    {
        List<EmployeeDto> GetEmployees();
        EmployeeDto GetEmployees(int id);
        EmployeeDto InsertEmployee(EmployeeDto employee);
    }
}
