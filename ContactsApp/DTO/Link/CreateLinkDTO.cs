using Employee.Domain;

namespace Contacts.WebService.DTO.Link
{
    public class CreateLinkDTO
    {
        public long EmployeeId { get; set; }
        public string Value { get; set; }
        public LinkType LinkType { get; set; }
    }
}