using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [MaxLength(50)]

        public string Name { get; set; }


        public int? Age { get; set; }


        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

  
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string ImageName { get; set; }




        // [ForeignKey("Department")]
        public int? DepartmentId { get; set; } // name referr that its foreign key
                                               // ? allow null cuz it gives error cuz i have employee in the database
        //navigational property one 
        public Department Department { get; set; } 
    }
}
