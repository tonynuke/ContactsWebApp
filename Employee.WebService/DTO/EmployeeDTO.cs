using System;
using System.Collections.Generic;

namespace Employee.WebService.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public DateTime BirthDate { get; set; }
        public List<ContactDTO> Contacts { get; set; } = new List<ContactDTO>();
    }
}