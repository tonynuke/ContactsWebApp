using System;
using System.Collections.Generic;
using Contacts.WebService.DTO.Link;

namespace Contacts.WebService.DTO.Employee
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public DateTime BirthDate { get; set; }

        public IReadOnlyList<CreateLinkDTO> Links { get; set; } = new List<CreateLinkDTO>();
    }
}