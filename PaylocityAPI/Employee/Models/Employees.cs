using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class Employees
    {
        public int EmployeeId { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage ="First Name is must")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Last Name is must")]
        public string LastName { get; set; }
        public int? Salary { get; set; }

        
        [Required(ErrorMessage = "Email ID is must")]
        [EmailAddress]
        public string EmailID { get; set; }
        public int childDependents { get; set; }

        public int spouseDependents { get; set; }

        public float specialDiscount { get; set; }

        public float totalDeduction { get; set; }

        public float TakeHome { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<Dependents> Dependents { get; set; }

    }
}
