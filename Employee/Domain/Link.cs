namespace Employee.Domain
{
    public enum LinkType
    {
        Skype,
        Email,
        Custom
    }

    public class Link
    {
        public long Id { get; set; }
        public LinkType Type { get; set; }
        public string Value { get; set; }

    }
}