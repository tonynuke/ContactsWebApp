namespace Employee.Domain
{
    public class Link
    {
        public long Id { get; private set; }
        public LinkType Type { get; set; }
        public string Value { get; set; }
    }
}