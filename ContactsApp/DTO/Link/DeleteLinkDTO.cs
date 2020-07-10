using System.Collections.Generic;

namespace ContactsApp.DTO.Link
{
    public class DeleteLinkDTO
    {
        public long EmployeeId { get; set; }
        public IReadOnlyList<long> LinkIds { get; set; } = new List<long>();
    }
}