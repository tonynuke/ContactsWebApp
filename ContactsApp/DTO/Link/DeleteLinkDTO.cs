using System.Collections.Generic;

namespace Contacts.WebService.DTO.Link
{
    public class DeleteLinkDTO
    {
        public long EmployeeId { get; set; }
        public IReadOnlyList<long> LinkIds { get; set; } = new List<long>();
    }
}