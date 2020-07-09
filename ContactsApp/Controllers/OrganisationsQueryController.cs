using System;
using System.Linq;
using Employee.DTO;
using Employee.Persistence;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Controllers
{
    [Route("[controller]")]
    public class OrganisationsQueryController : ODataController
    {
        private readonly ILogger<OrganisationsQueryController> logger;

        private readonly OrganisationQueryDbContext dbContext;

        [HttpGet]
        [EnableQuery]
        public IQueryable<OrganisationDTO> Get()
        {
            return this.dbContext.Organisations.AsNoTracking();
        }

        public OrganisationsQueryController(ILogger<OrganisationsQueryController> logger, OrganisationQueryDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
