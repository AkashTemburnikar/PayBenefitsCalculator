using PaylocityAPI.DBContext;
using PaylocityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity_API.Repository
{
    public interface IEmployeeRepo
    {
        Task<EmployeeDto> GetEmployeeAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeeAsync();
        Task<string> PostEmployeeAsync(Employee entityPost);

        Task<string> UpdateEmployeeAsync(Employee entityUpdate);
        Task<string> DeleteAsync(int id);
    }
}
