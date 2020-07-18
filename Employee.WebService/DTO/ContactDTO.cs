using System.ComponentModel.DataAnnotations;
using Employee.Domain.Contacts;

namespace Employees.WebService.DTO
{
    public class ContactDTO
    {
        [Required]
        public ContactType Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Value { get; set; }
    }
}