using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter name here")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Salary here")]
        public string Salary { get; set; }
        [Required(ErrorMessage = "Enter City here")]
        public string city { get; set; }
    }
}
