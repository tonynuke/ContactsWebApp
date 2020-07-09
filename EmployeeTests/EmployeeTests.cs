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
        private static OrganizationDbContext dbContext;

        [OneTimeSetUp]
        public Task OneTimeSetUp()
        {
            string connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Database=Organization;Initial Catalog=Organization";
            
            var optionsBuilder = new DbContextOptionsBuilder<OrganizationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            dbContext = new OrganizationDbContext(optionsBuilder.Options);

            return dbContext.Database.EnsureCreatedAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]
        public async Task AddOrganizationTest()
        {
            var dto = dbContext.Organizations.ToList();

            var Organization = new Organization("Рога и копыта");
            var a = await dbContext.Organizations.AddAsync(Organization);

            dbContext.SaveChanges();
        }
    }
}