using System.Collections.Generic;

namespace Contacts.WebService.DTO.Employee
{
    public class DeleteEmployeeDTO
    {
        public IReadOnlyList<long> Ids { get; set; } = new List<long>();
    }
}