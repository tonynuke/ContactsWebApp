using Employee.Domain;
using Employee.Domain.Contacts;

namespace Contacts.WebService.DTO.Contact
{
    public class CreateContactDTO
    {
        public string Value { get; set; }
        public ContactType Type { get; set; }
    }
}