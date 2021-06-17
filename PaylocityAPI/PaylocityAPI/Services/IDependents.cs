using PaylocityAPI.DBContext;
using PaylocityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity_API.Repository
{
    public interface IDependentsRepo 
    {
        float SelfCalculateBenifts(Employee employee);

        float DepCalculateBenifts(Employee employee);

        float CalculateDiscBenifts(Employee employee);

    }
}
