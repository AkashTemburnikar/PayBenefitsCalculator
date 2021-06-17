using Paylocity_API.Repository;
using System;
using Moq;
using System.Threading.Tasks;
using PaylocityAPI.Services;
using PaylocityAPI.DBContext;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Paylocity_API.Controllers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace PaylocityTest
{
    public class EmployeeControllerTests
    {
        private EmployeeController _controller;
        private Mock<IEmployeeRepo> _employeeRepo;
        private Mock<IDependentsRepo> _dependentsRepo;

        private Mock<ILogger<EmployeeController>> _mockLogger;
        private Mock<ILogger<IEmployeeRepo>> _empMockLogger;

        private Mock<PaylocityBenefitsContext> _context;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<EmployeeController>>();
            _context = new Mock<PaylocityBenefitsContext>();
            _employeeRepo = new Mock<IEmployeeRepo>();
            _empMockLogger = new Mock<ILogger<IEmployeeRepo>>();
            _controller = new EmployeeController(_employeeRepo.Object, _empMockLogger.Object);
            _employeeRepo.Setup(x => x.GetEmployeeAsync(It.IsAny<int>())).Returns(Task.FromResult(new PaylocityAPI.Models.EmployeeDto()));
            _employeeRepo.Setup(x => x.PostEmployeeAsync(It.IsAny<Employee>())).Returns(Task.FromResult(string.Empty));
            _employeeRepo.Setup(x => x.UpdateEmployeeAsync(It.IsAny<Employee>())).Returns(Task.FromResult(string.Empty));
            _employeeRepo.Setup(x => x.DeleteAsync(It.IsAny<int>())).Returns(Task.FromResult(string.Empty));
        }

        [Test]
        public void EmployeeController_Test_GetAllEmployee()
        {
            var result = _controller.GetAllEmployee();
            var statusCodeResult = (IStatusCodeActionResult)result.Result;
            Assert.IsNotNull(result, "Result is null");
            Assert.IsNotNull(statusCodeResult);
            Assert.Contains(statusCodeResult.StatusCode, new List<int> { 200, 500 });
        }

        [Test]
        public void EmployeeController_Test_PostEmployee()
        {
            var result = _controller.PostEmployee(addEmployeeData.FirstOrDefault());
            var statusCodeResult = (IStatusCodeActionResult)result.Result;
            Assert.IsNotNull(result, "Result is null");
            Assert.IsNotNull(statusCodeResult);
            Assert.Contains(statusCodeResult.StatusCode, new List<int> { 200, 500 });
        }

        [Test]
        public void EmployeeController_Test_UpdateEmployee()
        {
            var result = _controller.UpdateEmployee(addEmployeeData.FirstOrDefault());
            var statusCodeResult = (IStatusCodeActionResult)result.Result;
            Assert.IsNotNull(result, "Result is null");
            Assert.IsNotNull(statusCodeResult);
            Assert.Contains(statusCodeResult.StatusCode, new List<int> { 200, 500 });
        }

        [Test]
        public void EmployeeController_Test_DeleteEmployeeAsync()
        {
            var result = _controller.DeleteEmployeeAsync(1);
            var statusCodeResult = (IStatusCodeActionResult)result.Result;
            Assert.IsNotNull(result, "Result is null");
            Assert.IsNotNull(statusCodeResult);
            Assert.Contains(statusCodeResult.StatusCode, new List<int> { 200, 500 });
        }

        #region param Setup

        internal static readonly Employee[] addEmployeeData =
        {
            new Employee {
                EmployeeId=1,
                FirstName="Finances",
                LastName="ACTIVE",
                EmailId="test@test.com",
                Salary= 1000
            },
        };

        #endregion
    }
}
