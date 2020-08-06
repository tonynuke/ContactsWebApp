using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.AspNet.OData;
using Employee.WebService.DTO;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesService employeesService;

        private readonly IMapper mapper;

        [HttpGet]
        public Task<IQueryable<EmployeeDTO>> Get(ODataQueryOptions<EmployeeDTO> options)
        {
            var employees = this.employeesService.GetAll().AsNoTracking();
            return employees.GetQueryAsync(this.mapper, options);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO dto)
        {
            var result = await this.employeesService.CreateEmployee(dto);
            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] PutEmployeeDTO dto)
        {
            var result = await this.employeesService.UpdateEmployee(dto);
            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Error);
        }

        [HttpDelete]
        public Task DeleteEmployee([FromBody] DeleteEmployeeDTO dto)
        {
            return this.employeesService.DeleteEmployee(dto);
        }

        public EmployeesController(EmployeesService employeesService, IMapper mapper)
        {
            this.employeesService = employeesService ?? throw new ArgumentNullException(nameof(employeesService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
