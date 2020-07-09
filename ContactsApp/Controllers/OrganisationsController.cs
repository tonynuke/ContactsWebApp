using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactsApp.DTO;
using Employee.Domain;
using Employee.Persistence;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Controllers
{
    [Route("[controller]")]
    public class OrganisationsController : ControllerBase
    {
        private readonly ILogger<OrganisationsController> logger;

        private readonly OrganisationDbContext dbContext;

        private readonly IMapper mapper;

        [HttpGet]
        [EnableQuery]
        public IQueryable<Organisation> Get()
        {
            var request = this.dbContext.Organisations.AsNoTracking();

            //return request.ProjectTo<OrganisationDTO>(this.mapper.ConfigurationProvider);
            return request;
        }

        [HttpPost]
        [Route("organisation")]
        public async Task<long> CreateOrganisation([FromBody] CreateOrganisationDTO dto)
        {
            var organisation = new Organisation(dto.Name);
            await this.dbContext.Organisations.AddAsync(organisation);
            await this.dbContext.SaveChangesAsync();
            return organisation.Id;
        }

        public OrganisationsController(ILogger<OrganisationsController> logger, OrganisationDbContext dbContext,
            IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper =
                mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}