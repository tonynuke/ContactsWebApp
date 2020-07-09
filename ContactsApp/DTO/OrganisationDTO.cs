using System.Collections.Generic;

namespace ContactsApp.DTO
{
    public class OrganisationDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
    }
}