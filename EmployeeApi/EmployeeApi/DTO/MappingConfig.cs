using AutoMapper;
using EmployeeApi.Models;

namespace EmployeeApi.DTO
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig=new MapperConfiguration(Config=>
            {
                Config.CreateMap<Employees, EmployeeDto>();
                Config.CreateMap<EmployeeDto, Employees>();
            });
            return mappingConfig;
        }
    }
}
