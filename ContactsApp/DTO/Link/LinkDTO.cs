using Employee.Domain;

namespace Contacts.WebService.DTO.Link
{
    public class LinkDTO
    {
        public long Id { get; set; }
        public LinkType Type { get; set; }
        public string Value { get; set; }
    }
}