using ContactsApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Persistence
{
    public class PersonContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }

        public DbSet<Person> Users { get; set; }
    }
}
