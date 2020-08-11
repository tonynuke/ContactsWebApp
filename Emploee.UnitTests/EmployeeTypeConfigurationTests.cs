using System.Linq;
using Employee.Domain.Contacts;
using Employee.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Employee.UnitTests
{
    public class EmployeeTypeConfigurationTests
    {
        private static EmployeeDbContext dbContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            const string dataBaseName = "TestDB";
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(dataBaseName);
            dbContext = new EmployeeDbContext(optionsBuilder.Options);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public void Employee_SmokeTest()
        {
            var newEmployee = new Domain.Employee("Ivan");
            var contact = Contact.Create(ContactType.Skype, "live:nickname");
            newEmployee.AddContact(contact.Value);

            dbContext.Employees.Add(newEmployee);
            dbContext.SaveChanges();

            var queriedEmployee = dbContext.Employees.Single(employee => employee.Id == newEmployee.Id);
            Assert.IsNotNull(queriedEmployee);

            dbContext.Remove(newEmployee);

            dbContext.SaveChanges();
        }
    }
}