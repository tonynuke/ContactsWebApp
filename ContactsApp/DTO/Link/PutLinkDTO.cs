using Employee.Domain;

namespace ContactsApp.DTO.Link
{
    public class PutLinkDTO
    {
        public string Value { get; set; }
        public LinkType LinkType { get; set; }
        public long EmployeeId { get; set; }
        public long LinkId { get; set; }
    }
}