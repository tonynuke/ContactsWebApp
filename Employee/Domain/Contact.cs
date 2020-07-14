namespace Employee.Domain
{
    public class Contact
    {
        public long Id { get; private set; }
        public ContactType Type { get; set; }
        public string Value { get; set; }
    }
}