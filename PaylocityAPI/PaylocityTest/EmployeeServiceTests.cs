using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PaylocityAPI.DBContext;
using PaylocityAPI.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylocityTest
{
    class EmployeeServiceTests : IDisposable
    {
        private readonly DbContextOptions<PaylocityBenefitsContext> dbContextOptions = new DbContextOptionsBuilder<PaylocityBenefitsContext>()
            .UseInMemoryDatabase(databaseName: "PaylocityBenefits")
            .Options;

        PaylocityBenefitsContext context;
        private EmployeeService service;
        private DependentsService depService;
        readonly int expected = 0;

        [OneTimeSetUp]
        public void Setup()
        {
            depService = new DependentsService();
            service = new EmployeeService(new PaylocityBenefitsContext(dbContextOptions), depService);
            context = new PaylocityBenefitsContext(dbContextOptions);
            if (!context.Employee.Any())
            {
                MockSetup.seedData(context);
                context.SaveChanges();
            }
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            context.Dispose();
        }

        [Test]
        public async Task EmployeeService_Test_GetAllEmployee()
        {
            var result = await service.GetAllEmployeeAsync();
            Assert.IsNotNull(result);
            Assert.GreaterOrEqual(((IList)result).Count, expected);
            var okResult = result.As<object>();
            var employee = okResult.As<object>();
            employee.Should().NotBeNull();
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        public async Task EmployeeService_Test_GetEmployeeById(int id)
        {
            var result = await service.GetEmployeeAsync(id);
            Assert.IsNotNull(result);
            var okResult = result.As<object>();
            var employee = okResult.As<object>();
            employee.Should().NotBeNull();
        }

        [Test]
        [TestCase(1000)]
        [TestCase(2000)]
        public async Task EmployeeService_Test_GetEmployeeById_Negitive(int id)
        {
            var result = await service.GetEmployeeAsync(id);
            Assert.IsNull(result);
            var okResult = result.As<object>();
            var employee = okResult.As<object>();
            employee.Should().BeNull();
        }

        [Test, TestCaseSource(nameof(addEmployeeData))]
        public async Task EmployeeService_Test_PostEmployee(Employee employee)
        {
            var result = await service.PostEmployeeAsync(employee);
            Assert.IsNotNull(result);
            Assert.AreEqual("Inserted Sucessfully", result);
            var okResult = result.As<object>();
            var emp = okResult.As<object>();
            emp.Should().NotBeNull();
        }

        [Test, TestCaseSource(nameof(addEmployeeData))]
        public async Task EmployeeService_Test_PostEmployee_Negative(Employee employee)
        {
            employee = null;
            var result = await service.PostEmployeeAsync(employee);
            Assert.IsNull(result);
            Assert.AreEqual(null, result);
            var okResult = result.As<object>();
            var emp = okResult.As<object>();
            emp.Should().BeNull();
        }


        [Test, TestCaseSource(nameof(updateEmployeeData))]
        public async Task EmployeeService_Test_UpdateEmployee(Employee employee)
        {
            var result = await service.UpdateEmployeeAsync(employee);
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Sucessfully", result);
            var okResult = result.As<object>();
            var emp = okResult.As<object>();
            emp.Should().NotBeNull();
        }

        [Test, TestCaseSource(nameof(updateEmployeeData))]
        public async Task EmployeeService_Test_UpdateEmployee_Negative(Employee employee)
        {
            employee = null;
            var result = await service.UpdateEmployeeAsync(employee);
            Assert.IsNull(result);
            Assert.AreEqual(null, result);
            var okResult = result.As<object>();
            var emp = okResult.As<object>();
            emp.Should().BeNull();
        }

        [Test]
        [TestCase(1)]
        public async Task EmployeeService_Test_Delete(int id)
        {
            var result = await service.DeleteAsync(id);
            Assert.IsNotEmpty(result);
            var okResult = result.As<object>();
            var emp = okResult.As<object>();
            emp.Should().NotBeNull();
        }

        [Test]
        [TestCase(1000)]
        public async Task EmployeeService_Test_Delete_Negative(int id)
        {
            var result = await service.DeleteAsync(id);
            Assert.IsNull(result);
            var okResult = result.As<object>();
            var emp = okResult.As<object>();
            emp.Should().BeNull();
        }

        #region param Setup

        internal static readonly Employee[] addEmployeeData =
        {
            new Employee {
                EmployeeId=1,
                FirstName="Doug",
                LastName="Gizz",
                EmailId="add@test.com",
                Salary= 1000
            },
        };

        internal static readonly Employee[] updateEmployeeData =
        {
            new Employee {
                EmployeeId=3,
                FirstName="Steve",
                LastName="Dure",
                EmailId="Taylor@abc.com",
                Salary= 1000
            },
        };

        #endregion
    }
}