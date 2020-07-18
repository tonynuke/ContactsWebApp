using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee.WebService.DTO
{
    public class CreateEmployeeDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(100)]
        public string Patronymic { get; set; }

        [StringLength(100)]
        public string Organization { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        public DateTime BirthDate { get; set; }

        public IReadOnlyList<ContactDTO> Contacts { get; set; } = new List<ContactDTO>();
    }
}