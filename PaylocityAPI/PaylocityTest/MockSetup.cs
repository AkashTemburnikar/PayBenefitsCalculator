using PaylocityAPI.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaylocityTest
{
    public class MockSetup
    {
        public static void seedData(PaylocityBenefitsContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Employee.AddRange(
                new Employee() { FirstName = "John", LastName = "Doe", EmailId = "john@abc.com", Salary = 10000 },
                new Employee() { FirstName = "David", LastName = "Lesh", EmailId = "David@abc.com", Salary = 10000 },
                new Employee() { FirstName = "Sean", LastName = "Lee", EmailId = "Sean@abc.com", Salary = 10000 },
                new Employee() { FirstName = "Taylor", LastName = "S", EmailId = "Taylor@abc.com", Salary = 10000 }
            );

            context.Dependents.AddRange(
                new Dependents() { EmployeeId = 1, DepTypeId = 1, Name = "Test1", CreatedDate = DateTime.Now },
                new Dependents() { EmployeeId = 4, DepTypeId = 1, Name = "Test1", CreatedDate = DateTime.Now }
            );
            context.DependentType.AddRange(
                new DependentType() { Type = "Child", Yearly = 500 },
                new DependentType() { Type = "Spouse", Yearly = 500 }
            );
            context.SaveChanges();

        }
    }
}
