﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.DTO
{
    public class RegRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
       
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Role { get; set; }

    }
}
