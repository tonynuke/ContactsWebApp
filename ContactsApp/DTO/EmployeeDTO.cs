using System;
using System.Collections.Generic;

namespace ContactsApp.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }

        public string Position { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BirthDate { get; set; }

        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}