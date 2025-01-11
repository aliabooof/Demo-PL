using Demo.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace DEMO_PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required !")]
        [MaxLength(50,ErrorMessage ="Max length is 50 chars")]
        [MinLength(10,ErrorMessage ="Min length is 10 chars")]
        public string Name { get; set; }

        [Range(22,30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$"
            ,ErrorMessage ="address must be like 123-street-city-country")]
        public string Address { get; set; }

    
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }

        public DateTime HireDate { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
        public int? DepartmentId { get; set; } // name referr that its foreign key
                                               // ? allow null cuz it gives error cuz i have employee in the database
        //navigational property one 
        public Department Department { get; set; } 
    }
}
