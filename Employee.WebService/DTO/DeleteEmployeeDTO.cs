using System.ComponentModel.DataAnnotations;

namespace Employee.WebService.DTO
{
    public class DeleteEmployeeDTO
    {
        [Required]
        public long Id { get; set; }
    }
}