using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.AspNet.OData;
using ContactsApp.DTO;
using Employee.Domain;
using Employee.Persistence;
using Microsoft.AspNet.OData.Query;
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
        public Task<IQueryable<OrganisationDTO>> Get(ODataQueryOptions<OrganisationDTO> options)
        {
            var request = this.dbContext.Organisations.AsNoTracking();
            return request.GetQueryAsync(this.mapper, options);
        }

        [HttpPost]
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