using System.Collections.Generic;

namespace ContactsApp.DTO.Employee
{
    public class DeleteEmployeeDTO
    {
        public IReadOnlyList<long> Ids { get; set; } = new List<long>();
    }
}