using Employee.Domain;

namespace Contacts.WebService.DTO.Link
{
    public class PutLinkDTO
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string Value { get; set; }
        public LinkType LinkType { get; set; }
    }
}