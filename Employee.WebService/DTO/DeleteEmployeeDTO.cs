using System.ComponentModel.DataAnnotations;

namespace Employees.WebService.DTO
{
    public class DeleteEmployeeDTO
    {
        [Required]
        public long Id { get; set; }
    }
}