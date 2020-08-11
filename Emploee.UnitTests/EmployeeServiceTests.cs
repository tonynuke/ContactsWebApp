using System.Collections.Generic;
using System.Threading.Tasks;
using Employee.Domain.Contacts;
using Employee.Persistence;
using Employee.WebService;
using Employee.WebService.DTO;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Employee.UnitTests
{
    public class EmployeeServiceTests
    {
        public EmployeeDbContext GetDbContext()
        {
            const string dataBaseName = "TestDB";
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(dataBaseName);
            return new EmployeeDbContext(optionsBuilder.Options);
        }

        [Test]
        public async Task CreateEmployee_NameNotSpecified_Fail()
        {
            var dto = new CreateEmployeeDTO();
            await using var dbContext = GetDbContext();
            var service = new EmployeesService(dbContext);
            var result = await service.CreateEmployee(dto);

            Assert.IsTrue(result.IsFailure);
        }

        [Test]
        public async Task CreateEmployee_NameSpecified_ContactsEmpty_Success()
        {
            var dto = new CreateEmployeeDTO { Name = "name" };
            await using var dbContext = GetDbContext();
            var service = new EmployeesService(dbContext);
            var result = await service.CreateEmployee(dto);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task CreateEmployee_InvalidContacts_Fail()
        {
            var dto = new CreateEmployeeDTO
            {
                Name = "name",
                Contacts = new List<ContactDTO>
                {
                    new ContactDTO { Type = ContactType.Email}
                }
            };

            await using var dbContext = GetDbContext();
            var service = new EmployeesService(dbContext);
            var result = await service.CreateEmployee(dto);

            Assert.IsTrue(result.IsFailure);
        }
    }
}