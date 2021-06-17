using Microsoft.EntityFrameworkCore;
using Paylocity_API.Repository;
using PaylocityAPI.DBContext;
using PaylocityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityAPI.Services
{
    public class EmployeeService : IEmployeeRepo
    {
        private readonly PaylocityBenefitsContext _context;

        private readonly IDependentsRepo _depRepo;

        public EmployeeService(PaylocityBenefitsContext _context, IDependentsRepo _depRepo)
        {
            this._context = _context;
            this._depRepo = _depRepo;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(int id)
        {
            var employee = await _context.Employee
                                 .Include(c => c.Dependents)
                                 .Where(c => c.EmployeeId == id).FirstOrDefaultAsync();

            if (employee != null)
            {
                float selfBenDeduc = _depRepo.SelfCalculateBenifts(employee);

                float depBenDeduc = _depRepo.DepCalculateBenifts(employee);

                float specialDisc = _depRepo.CalculateDiscBenifts(employee);

                var childData = employee.Dependents.Where(c => c.DepTypeId == 1).ToList();
                var spouseData = employee.Dependents.Where(c => c.DepTypeId == 2).ToList();

                var empdetials = new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    EmailID = employee.EmailId,
                    childDependents = childData.Count,
                    spouseDependents = spouseData.Count,
                    specialDiscount = (float)specialDisc,
                    totalDeduction = depBenDeduc + selfBenDeduc - specialDisc,
                    TakeHome = (float)(employee.Salary - depBenDeduc - selfBenDeduc + specialDisc)
                };

                List<Dependents> dList = new List<Dependents>();

                foreach (var name in childData)
                {
                    var dpObj = new Dependents();
                    dpObj.Name = name.Name;
                    dpObj.DepTypeId = name.DepTypeId;
                    dList.Add(dpObj);
                }
                foreach (var name in spouseData)
                {
                    var dpObj = new Dependents();
                    dpObj.Name = name.Name;
                    dpObj.DepTypeId = name.DepTypeId;
                    dList.Add(dpObj);
                }

                empdetials.Dependents = dList;
                return empdetials;
            }
            return null;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeeAsync()
        {
            var empdetials = await _context.Employee
                                 .Include(c => c.Dependents)
                                 .Select(s => new EmployeeDto()
                                 {
                                     EmployeeId = s.EmployeeId,
                                     FirstName = s.FirstName,
                                     LastName = s.LastName,
                                     EmailID = s.EmailId,
                                     childDependents = s.Dependents.Where(c => c.DepTypeId == 1).Count(),
                                     spouseDependents = s.Dependents.Where(c => c.DepTypeId == 2).Count()
                                 }).ToListAsync();
            if (empdetials != null)
            {
                return empdetials;
            }
            return null;
        }

        public async Task<string> PostEmployeeAsync(Employee empDto)
        {
            if (empDto != null)
            {
                Employee emp = new Employee();
                emp.FirstName = empDto.FirstName;
                emp.LastName = empDto.LastName;
                emp.EmailId = empDto.EmailId;

                var checkMail = await _context.Employee.Where(c => c.EmailId == empDto.EmailId).ToListAsync();
                if (checkMail.Count == 0)
                {
                    _context.Employee.Add(emp);
                    _context.SaveChanges();
                    commonFunction(empDto.Dependents.ToList(), empDto.EmailId);
                    return "Inserted Sucessfully";
                }
                else
                {
                    return "Email Id already exist";
                }
            }
            return null;
        }

        public async Task<string> UpdateEmployeeAsync(Employee entityToUpdate)
        {
            if (entityToUpdate != null)
            {
                var empDetails = _context.Employee.Where(c => c.EmployeeId == entityToUpdate.EmployeeId).FirstOrDefault();
                empDetails.FirstName = entityToUpdate.FirstName;
                empDetails.LastName = entityToUpdate.LastName;
                empDetails.EmailId = entityToUpdate.EmailId;


                _context.Employee.Update(empDetails);

                var checkMail = _context.Employee.Where(c => c.EmailId == entityToUpdate.EmailId).ToList();
                if (checkMail.Count == 1)
                {

                    var empDepDetails = _context.Dependents.Where(c => c.EmployeeId == entityToUpdate.EmployeeId).ToList();

                    _context.Dependents.RemoveRange(empDepDetails);

                    commonFunction(entityToUpdate.Dependents.ToList(), empDetails.EmailId);
                    await _context.SaveChangesAsync();
                    return "Updated Sucessfully";
                }
                else
                {
                    return "Email Id already exist";
                }
            }
            return null;
        }

        public void commonFunction(List<Dependents> deps, string emailid = null)
        {
            int employeeID = (from e in _context.Employee where e.EmailId == emailid select e.EmployeeId).FirstOrDefault();

            foreach (var item in deps)
            {
                Dependents dep = new Dependents();
                dep.EmployeeId = employeeID;
                dep.DepTypeId = item.DepTypeId;
                dep.Name = item.Name;
                _context.Dependents.Add(dep);
            }
            _context.SaveChanges();
        }

        public async Task<string> DeleteAsync(int id)
        {
            var employee = await _context.Employee
                               .Include(c => c.Dependents)
                               .Where(c => c.EmployeeId == id).FirstOrDefaultAsync();

            if (employee != null)
            {
                var depData = employee.Dependents.ToList();
                foreach (var data in depData)
                {
                    _context.Dependents.Remove(data);
                }
                _context.SaveChanges();

                _context.Employee.Remove(employee);
                _context.SaveChanges();
                return "Successfully Deleted";
            }
            return null;
        }
    }
}
