using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee.WebService.DTO
{
    public class CreateEmployeeDTO
    {
        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Organization { get; set; }

        public string Position { get; set; }

        public DateTime BirthDate { get; set; }

        public IReadOnlyList<ContactDTO> Contacts { get; set; } = new List<ContactDTO>();
    }
}