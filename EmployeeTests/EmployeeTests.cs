using System.Linq;
using System.Threading.Tasks;
using Employee.Domain;
using Employee.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Employee.IntegrationTests
{
    public class Tests
    {
        private static OrganisationDbContext dbContext;

        [OneTimeSetUp]
        public Task OneTimeSetUp()
        {
            string connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Database=Organisation;Initial Catalog=Organisation";
            
            var optionsBuilder = new DbContextOptionsBuilder<OrganisationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            dbContext = new OrganisationDbContext(optionsBuilder.Options);

            return dbContext.Database.EnsureCreatedAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]
        public async Task AddOrganisationTest()
        {
            var dto = dbContext.EmployeeDTOs.ToList();

            var organisation = new Organisation("Рога и копыта");
            var a = await dbContext.Organisations.AddAsync(organisation);

            dbContext.SaveChanges();
        }
    }
}