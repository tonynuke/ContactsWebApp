using Employee.Domain;
using Microsoft.EntityFrameworkCore;

namespace Employee.Persistence
{
    public class OrganisationContext : DbContext
    {
        public OrganisationContext() : base()
        {

        }

        public DbSet<Organisation> Users { get; set; }
    }
}
