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
    public class EmployeesQueryController : ODataController
    {
        private readonly ILogger<EmployeesQueryController> logger;

        private readonly OrganisationQueryDbContext dbContext;

        [HttpGet]
        [EnableQuery]
        public IQueryable<PlainEmployeeDTO> Get()
        {
            return this.dbContext.Employees.AsNoTracking();
        }

        public EmployeesQueryController(ILogger<EmployeesQueryController> logger, OrganisationQueryDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
