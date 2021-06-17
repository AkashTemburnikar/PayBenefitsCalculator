using PaylocityAPI.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityAPI.Models
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }

        public List<Dependents> Dependents { get; set; }

        public int childDependents { get; set; }

        public int spouseDependents { get; set; }

        public float specialDiscount { get; set; }

        public float totalDeduction { get; set; }

        public float TakeHome { get; set; }
    }
}
