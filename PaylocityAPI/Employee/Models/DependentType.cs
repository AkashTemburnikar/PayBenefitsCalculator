using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class DependentType
    {
        public int DependentTypeId { get; set; }
        public string Type { get; set; }
        public int Yearly { get; set; }
        public List<Dependents> Dependents { get; set; }

    }
}
