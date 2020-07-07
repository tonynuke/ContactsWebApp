using System;
using System.Linq;
using Employee.Domain;
using Employee.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EmployeeTests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        [Test]
        public void Test1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrganisationDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=Organisation;Initial Catalog=Organisation");

            using (var dbContext = new OrganisationDbContext(optionsBuilder.Options))
            {
                var a = dbContext.Database.CanConnect();
                var b = dbContext.Database.EnsureCreated();
                //dbContext.Organisations.w;

                var org = new Organisation("org");
                org.CreateEmployee("name", "position");

                dbContext.Organisations.Add(org);
                dbContext.SaveChanges();
            }
        }
    }
}