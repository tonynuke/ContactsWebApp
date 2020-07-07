using System.Collections.Generic;
using Employee.Domain;

namespace Employee.DTO
{
    public class EmployeeDTO
    {
        public string OrganisationName { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string BirthDate { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}