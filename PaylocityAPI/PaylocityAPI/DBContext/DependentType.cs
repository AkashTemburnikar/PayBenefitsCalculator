using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PaylocityAPI.DBContext
{
    public partial class DependentType
    {
        public DependentType()
        {
            Dependents = new HashSet<Dependents>();
        }

        public int DependentTypeId { get; set; }
        public string Type { get; set; }
        public int Yearly { get; set; }

        public virtual ICollection<Dependents> Dependents { get; set; }
    }
}
