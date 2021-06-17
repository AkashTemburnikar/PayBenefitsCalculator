using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Paylocity_API.Repository;
using PaylocityAPI.DBContext;
using PaylocityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _employeeRepository;
        private readonly ILogger<IEmployeeRepo> _logger;

        public EmployeeController(IEmployeeRepo _employeeRepository, ILogger<IEmployeeRepo> _logger)
        {
            this._employeeRepository = _employeeRepository;
            this._logger = _logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            try
            {
                var emp = await _employeeRepository.GetEmployeeAsync(id);
                if (emp == null)
                {
                    return NotFound();
                }
                return Ok(emp);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Couldn't fetch employee");
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var allEmp = await _employeeRepository.GetAllEmployeeAsync();
                if (allEmp == null)
                {
                    return NotFound();
                }
                return Ok(allEmp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Couldn't fetch All employee");
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee empDto)
        {
            try
            {
                var saveEmp = await _employeeRepository.PostEmployeeAsync(empDto);
                if (saveEmp == null)
                {
                    return NotFound();
                }
                return Ok(saveEmp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Couldn't Add employee");
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            try
            {
                var updateEmp = await _employeeRepository.UpdateEmployeeAsync(employee);
                if (updateEmp == null)
                {
                    return NotFound();
                }
                return Ok(updateEmp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Couldn't Update employee");
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            try
            {
                var deleteEmp = await _employeeRepository.DeleteAsync(id);
                if (deleteEmp == null)
                {
                    return NotFound();
                }
                return Ok(deleteEmp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Couldn't Delete employee");
                throw new Exception(ex.Message);
            }
        }
    }
}

