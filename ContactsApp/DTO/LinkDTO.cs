using Employee.Domain;

namespace ContactsApp.DTO
{
    public class LinkDTO
    {
        public long Id { get; set; }
        public LinkType Type { get; set; }
        public string Value { get; set; }
    }
}