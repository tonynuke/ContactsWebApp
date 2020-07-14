using Employee.Domain;
using Employee.Domain.Contacts;

namespace Contacts.WebService.DTO.Contact
{
    public class ContactDTO
    {
        public long Id { get; set; }
        public ContactType Type { get; set; }
        public string Value { get; set; }
    }
}