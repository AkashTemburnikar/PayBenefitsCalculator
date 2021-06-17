using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class Dependents
    {
        public int DependentId { get; set; }
        public int EmployeeId { get; set; }
        public int DepTypeId { get; set; }
        public string Name { get; set; }
        public  DependentType DepType { get; set; }
        public  Employees Employee { get; set; }
    }
}
