using Employee.Domain;

namespace Contacts.WebService.DTO.Contact
{
    public class CreateContactDTO
    {
        public string Value { get; set; }
        public ContactType Type { get; set; }
    }
}