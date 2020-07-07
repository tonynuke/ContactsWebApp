using System;
using System.Linq;
using Employee.Persistence;

namespace Employee.Services
{
    public class OrganisationService
    {
        private readonly OrganisationDbContext organisationDbContext;

        public OrganisationService(OrganisationDbContext organisationDbContext)
        {
            this.organisationDbContext =
                organisationDbContext ?? throw new ArgumentNullException(nameof(organisationDbContext));
        }
    }
}