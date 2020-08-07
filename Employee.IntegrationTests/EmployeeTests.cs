using System.Linq;
using System.Threading.Tasks;
using Employee.Domain.Contacts;
using Employee.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Employee.IntegrationTests
{
    public class Tests
    {
        private static EmployeeDbContext dbContext;

        [OneTimeSetUp]
        public Task OneTimeSetUp()
        {
            string connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Database=Test;Initial Catalog=Test";
            
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            dbContext = new EmployeeDbContext(optionsBuilder.Options);

            return dbContext.Database.EnsureCreatedAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]
        public async Task AddEmployee_SmokeTest()
        {
            var dto = dbContext.Employees.ToList();

            var employee = new Domain.Employee("Ivan");
            var contact = Contact.Create(ContactType.Skype, "live:nickname");
            employee.AddContact(contact.Value);

            await dbContext.Employees.AddAsync(employee);

            await dbContext.SaveChangesAsync();

            dbContext.Remove(employee);

            await dbContext.SaveChangesAsync();
        }
    }
}