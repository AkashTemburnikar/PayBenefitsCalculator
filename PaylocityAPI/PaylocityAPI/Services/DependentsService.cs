using Paylocity_API.Repository;
using PaylocityAPI.DBContext;
using PaylocityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityAPI.Services
{
    public class DependentsService : IDependentsRepo
    {
        public float DepCalculateBenifts(Employee employee)
        {
            var childCount = employee.Dependents.Where(c => c.DepTypeId == 1).Count();
            var spouseCount = employee.Dependents.Where(c => c.DepTypeId == 2).Count();

            float depBen = ((childCount + spouseCount) * PaycheckConstants.DepYearlyBenifts);
            depBen /= 26;

            return depBen;
        }

        public float CalculateDiscBenifts(Employee employee)
        {
            var childNameswithA = employee.Dependents.Where(c => c.DepTypeId == 1 && c.Name.StartsWith("a", StringComparison.InvariantCultureIgnoreCase)).Count();
            var spouseNameswithA = employee.Dependents.Where(c => c.DepTypeId == 2 && c.Name.StartsWith("a", StringComparison.InvariantCultureIgnoreCase)).Count();

            float val = 0;
            if (employee.FirstName.StartsWith("a", StringComparison.InvariantCultureIgnoreCase))
            {
                val = (float)(PaycheckConstants.DiscountWithA * PaycheckConstants.YearlyBenifts);
                val = val / 26;
            }

            float specialDisc = (childNameswithA + spouseNameswithA);
            specialDisc = (float)(specialDisc * (PaycheckConstants.DiscountWithA * PaycheckConstants.DepYearlyBenifts)) / 26;

            return specialDisc + val;
        }

        public float SelfCalculateBenifts(Employee employee)
        {
            float val = (float)(PaycheckConstants.YearlyBenifts)/ 26;
            return val;
        }
    }
}
