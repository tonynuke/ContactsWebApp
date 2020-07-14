using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contacts.WebService.DTO
{
    public class PutEmployeeDTO
    {
        [Required]
        public long Id { get; set; }

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