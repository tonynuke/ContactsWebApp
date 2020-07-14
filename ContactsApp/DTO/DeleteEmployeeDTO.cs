using System.ComponentModel.DataAnnotations;

namespace Contacts.WebService.DTO
{
    public class DeleteEmployeeDTO
    {
        [Required]
        public long Id { get; set; }
    }
}