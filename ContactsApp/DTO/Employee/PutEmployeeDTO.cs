using System;
using System.Collections.Generic;
using Contacts.WebService.DTO.Link;

namespace Contacts.WebService.DTO.Employee
{
    public class PutEmployeeDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public DateTime BirthDate { get; set; }
        public IReadOnlyList<PutLinkDTO> Links { get; set; } = new List<PutLinkDTO>();
    }
}