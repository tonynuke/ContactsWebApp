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
    public class OrganizationsController : ControllerBase
    {
        private readonly ILogger<OrganizationsController> logger;

        private readonly OrganizationDbContext dbContext;

        private readonly IMapper mapper;

        private readonly OrganizationService service;

        [HttpGet]
        public Task<IQueryable<OrganizationDTO>> Get(ODataQueryOptions<OrganizationDTO> options)
        {
            var request = this.dbContext.Organizations.AsNoTracking();
            return request.GetQueryAsync(this.mapper, options);
        }

        [HttpPost]
        public async Task<long> CreateOrganization([FromBody] CreateOrganizationDTO dto)
        {
            var organization = new Organization(dto.Name);
            await this.dbContext.Organizations.AddAsync(organization);
            await this.dbContext.SaveChangesAsync();
            return organization.Id;
        }

        public OrganizationsController(ILogger<OrganizationsController> logger, OrganizationDbContext dbContext,
            IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mapper =
                mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}