using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PaylocityAPI.DBContext
{
    public partial class Dependents
    {
        public int DependentId { get; set; }
        public int EmployeeId { get; set; }
        public int DepTypeId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual DependentType DepType { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
